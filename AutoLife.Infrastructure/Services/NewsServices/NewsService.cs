using AutoLife.Application.DTOs.NewsDTOs;
using AutoLife.Domain.Entities;
using AutoLife.Persistence.DataBaseContext;
using AutoLife.Persistence.Repositories;
using AutoLife.Persistence.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoLife.Infrastructure.Services.NewsServices;

public class NewsService : INewsService
{
    private readonly IUnitOfWork<AppDbContext> _unitOfWork;
    private readonly IGenericRepository<News, AppDbContext> _newsRepository;

    public NewsService(IUnitOfWork<AppDbContext> unitOfWork, IGenericRepository<News, AppDbContext> newsRepository)
    {
        _unitOfWork = unitOfWork;
        _newsRepository = newsRepository;
    }

    public async Task AddNewsAsync(NewsCreateDto newsDto)
    {
        if (newsDto == null)
            throw new ArgumentNullException(nameof(newsDto), "News cannot be null.");
        var news = new News
        {
            Id = Guid.NewGuid(),
            Title = newsDto.Title,
            Body = newsDto.Body,
            FuelStationId = newsDto.FuelStationId,
            CompanyId = newsDto.CompanyId,
            ParkingId = newsDto.ParkingId,
            ServiceCenterId = newsDto.ServiceCenterId,
            UserId = newsDto.UserId,
            CreateDate = DateTime.UtcNow,
        };
        await _newsRepository.AddAsync(news);
        await _unitOfWork.SaveChangesAsync();
    }

    public async Task DeleteNewsAsync(Guid id)
    {
        if (id == Guid.Empty)
            throw new ArgumentException("Invalid news ID", nameof(id));
        var news = await _newsRepository.GetByIdAsync(id, asNoTracking: true);
        if (news == null)
            throw new KeyNotFoundException($"News with ID {id} not found.");
        news.IsDeleted = true; // Soft delete
        news.DeleteDate = DateTime.UtcNow;
        await _newsRepository.SoftDeleteAsync(news.Id);
        await _unitOfWork.SaveChangesAsync();
    }

    public async Task<IEnumerable<News>> GetAllNewsAsync()
    {
        var newsList = await _newsRepository.FindAsync(n => !n.IsDeleted);
        return newsList.ToList();
    }

    public async Task<IEnumerable<News>> GetLatestNewsAsync(int count = 5)
    {
        if (count <= 0)
            throw new ArgumentException("Count must be greater than zero.", nameof(count));
        var newsList = await _newsRepository.GetPagedListAsync(
            pageNumber: 1,
            pageSize: count,
            predicate: n => !n.IsDeleted,
            asNoTracking: true);
        return newsList.ToList();
    }

    public async Task<IEnumerable<News>> GetNewsByCategoryIdAsync(Guid categoryId)
    {
        if (categoryId == Guid.Empty)
            throw new ArgumentException("Invalid category ID", nameof(categoryId));
        var newsList = await _newsRepository.FindAsync(n => n.UserId == categoryId && !n.IsDeleted);
        return newsList.ToList();
    }

    public async Task<News> GetNewsByIdAsync(Guid id)
    {
        if (id == Guid.Empty)
            throw new ArgumentException("Invalid news ID", nameof(id));

        var news = await _newsRepository.GetByIdAsync(id, asNoTracking: true);

        if (news == null)
            throw new KeyNotFoundException($"News with ID {id} not found.");

        return news;
    }

    public async Task UpdateNewsAsync(Guid id, NewsCreateDto newsDto)
    {
        if (id == Guid.Empty)
            throw new ArgumentException("Invalid news ID", nameof(id));
        if (newsDto == null)
            throw new ArgumentNullException(nameof(newsDto), "News cannot be null.");

        var news = await _newsRepository.GetByIdAsync(id, asNoTracking: true);
        if (news == null)
            throw new KeyNotFoundException($"News with ID {id} not found.");

        news.Title = newsDto.Title;
        news.Body = newsDto.Body;
        news.FuelStationId = newsDto.FuelStationId;
        news.CompanyId = newsDto.CompanyId;
        news.ParkingId = newsDto.ParkingId;
        news.ServiceCenterId = newsDto.ServiceCenterId;
        news.UserId = newsDto.UserId;

        _newsRepository.Update(news);
        await _unitOfWork.SaveChangesAsync();
    }
}
