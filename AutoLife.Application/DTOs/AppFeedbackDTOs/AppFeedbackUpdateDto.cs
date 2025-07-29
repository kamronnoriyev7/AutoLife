using AutoLife.Domain.Enums;

namespace AutoLife.Application.DTOs.AppFeedbackDTOs;

public class AppFeedbackUpdateDto
{
    public Guid Id { get; set; }
    public string Message { get; set; } = default!;
    public FeedbackType Type { get; set; }

}
