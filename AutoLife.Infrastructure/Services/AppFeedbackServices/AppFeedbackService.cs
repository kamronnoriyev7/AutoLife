using AutoLife.Application.DTOs.AppFeedbackDTOs;
using AutoLife.Domain.Entities;
using AutoLife.Domain.Enums;
using AutoLife.Persistence.DataBaseContext;
using AutoLife.Persistence.Repositories;
using AutoLife.Persistence.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using SendGrid.Helpers.Errors.Model;

namespace AutoLife.Infrastructure.Services.AppFeedbackServices;

public class AppFeedbackService : IAppFeedbackService
{
    private readonly IUnitOfWork<AppDbContext> _unitOfWork;
    private readonly IGenericRepository<AppFeedback, AppDbContext> _appFeedbackRepository;

    public AppFeedbackService(IUnitOfWork<AppDbContext> unitOfWork, IGenericRepository<AppFeedback, AppDbContext> appFeedbackRepository)
    {
        _unitOfWork = unitOfWork;
        _appFeedbackRepository = appFeedbackRepository;
    }

    public async Task<int> CountAsync()
    {
        return await _appFeedbackRepository.CountAsync();
    }

    public async Task<AppFeedbackResponseDto> CreateAsync(AppFeedbackCreateDto dto)
    {
        var feedback = new AppFeedback
        {
            Id = Guid.NewGuid(),
            UserId = dto.UserId,
            Message = dto.Message,
            Type = dto.Type,
            SentAt = DateTime.UtcNow,
            CreateDate = DateTime.UtcNow
        };

        await _appFeedbackRepository.AddAsync(feedback);
        await _unitOfWork.SaveChangesAsync();

        return new AppFeedbackResponseDto
        {
            Id = feedback.Id,
            UserId = feedback.UserId,
            Message = feedback.Message,
            Type = feedback.Type,
            SentAt = feedback.SentAt
        };
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var feedback = await _appFeedbackRepository.GetByIdAsync(id);
        if (feedback == null)
            throw new NotFoundException("Fikr topilmadi");

        _appFeedbackRepository.Remove(feedback);
        await _unitOfWork.SaveChangesAsync();
        return true;
    }

    public async Task<IEnumerable<AppFeedbackResponseDto>> GetAllAsync(bool includeDeleted = false)
    {
        var feedbacks = await _appFeedbackRepository.FindAsync(f => includeDeleted || !f.IsDeleted);

        return feedbacks.Select(f => new AppFeedbackResponseDto
        {
            Id = f.Id,
            UserId = f.UserId,
            Message = f.Message,
            Type = f.Type,
            SentAt = f.SentAt
        });
    }

    public async Task<AppFeedbackResponseDto> GetByIdAsync(Guid id)
    {
        var feedback = await _appFeedbackRepository.GetByIdAsync(id);
        if (feedback == null || feedback.IsDeleted)
            throw new NotFoundException("Fikr topilmadi");

        return new AppFeedbackResponseDto
        {
            Id = feedback.Id,
            UserId = feedback.UserId,
            Message = feedback.Message,
            Type = feedback.Type,
            SentAt = feedback.SentAt
        };
    }

    public async Task<IEnumerable<AppFeedbackResponseDto>> GetByTypeAsync(FeedbackType type)
    {
        var feedbacks = await _appFeedbackRepository.FindAsync(f => f.Type == type && !f.IsDeleted);

        return feedbacks.Select(f => new AppFeedbackResponseDto
        {
            Id = f.Id,
            UserId = f.UserId,
            Message = f.Message,
            Type = f.Type,
            SentAt = f.SentAt
        });
    }

    public async Task<IEnumerable<AppFeedbackResponseDto>> GetByUserIdAsync(Guid userId)
    {
        var feedbacks = await _appFeedbackRepository.FindAsync(f => f.UserId == userId && !f.IsDeleted);

        return feedbacks.Select(f => new AppFeedbackResponseDto
        {
            Id = f.Id,
            UserId = f.UserId,
            Message = f.Message,
            Type = f.Type,
            SentAt = f.SentAt
        });
    }

    public async Task<bool> SoftDeleteAsync(Guid id)
    {
        var feedback = await _appFeedbackRepository.GetByIdAsync(id);
        if (feedback == null || feedback.IsDeleted)
            throw new NotFoundException("Fikr topilmadi");

        feedback.IsDeleted = true;
        feedback.DeleteDate = DateTime.UtcNow;

        _appFeedbackRepository.Update(feedback);
        await _unitOfWork.SaveChangesAsync();
        return true;
    }

    public async Task<AppFeedbackResponseDto> UpdateAsync(Guid id, AppFeedbackUpdateDto dto)
    {
        var feedback = await _appFeedbackRepository.GetByIdAsync(id);
        if (feedback == null || feedback.IsDeleted)
            throw new NotFoundException("Fikr topilmadi");

        feedback.Message = dto.Message ?? feedback.Message;
        feedback.Type = dto.Type;
        feedback.UpdateDate = DateTime.UtcNow;

        _appFeedbackRepository.Update(feedback);
        await _unitOfWork.SaveChangesAsync();

        return new AppFeedbackResponseDto
        {
            Id = feedback.Id,
            UserId = feedback.UserId,
            Message = feedback.Message,
            Type = feedback.Type,
            SentAt = feedback.SentAt
        };
    }
}
