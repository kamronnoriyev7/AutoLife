using AutoLife.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoLife.Application.DTOs.AppFeedbackDTOs;

public class AppFeedbackResponseDto
{
    public Guid Id { get; set; }

    public Guid UserId { get; set; }

    public string Message { get; set; } = string.Empty;

    public DateTime SentAt { get; set; }

    public FeedbackType Type { get; set; }

    // Agar foydalanuvchi haqida qisqacha info kerak bo‘lsa, quyidagicha qo‘shsa bo‘ladi:
    // public string? UserFullName { get; set; }
}
