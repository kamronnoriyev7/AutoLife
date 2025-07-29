using AutoLife.Application.DTOs.AppFeedbackDTOs;
using AutoLife.Domain.Enums;

namespace AutoLife.Infrastructure.Services.AppFeedbackServices;

public interface IAppFeedbackService
{
    Task<AppFeedbackResponseDto> CreateAsync(AppFeedbackCreateDto dto);
    Task<AppFeedbackResponseDto> GetByIdAsync(Guid id);
    Task<IEnumerable<AppFeedbackResponseDto>> GetAllAsync(bool includeDeleted = false);
    Task<bool> DeleteAsync(Guid id);
    Task<bool> SoftDeleteAsync(Guid id);    
    Task<AppFeedbackResponseDto> UpdateAsync(Guid id, AppFeedbackUpdateDto dto);
    Task<IEnumerable<AppFeedbackResponseDto>> GetByUserIdAsync(Guid userId);
    Task<IEnumerable<AppFeedbackResponseDto>> GetByTypeAsync(FeedbackType type);
    Task<int> CountAsync();
}