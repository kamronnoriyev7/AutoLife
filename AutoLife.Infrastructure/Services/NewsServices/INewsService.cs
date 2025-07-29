using AutoLife.Application.DTOs.NewsDTOs;
using AutoLife.Domain.Entities;

namespace AutoLife.Infrastructure.Services.NewsServices;

public interface INewsService
{
    Task AddNewsAsync(NewsCreateDto newsDto);
    Task UpdateNewsAsync(Guid id, NewsCreateDto newsDto);
    Task DeleteNewsAsync(Guid id);
    Task<News> GetNewsByIdAsync(Guid id);
    Task<IEnumerable<News>> GetAllNewsAsync();
    Task<IEnumerable<News>> GetNewsByCategoryIdAsync(Guid categoryId);
    Task<IEnumerable<News>> GetLatestNewsAsync(int count = 5);
}