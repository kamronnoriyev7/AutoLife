using AutoLife.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoLife.Application.DTOs.AppFeedbackDTOs;

public class AppFeedbackCreateDto
{
    public Guid UserId { get; set; }
    public string Message { get; set; } = string.Empty;
    public FeedbackType Type { get; set; }
}
