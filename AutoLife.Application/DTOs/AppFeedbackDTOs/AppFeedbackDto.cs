using AutoLife.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoLife.Application.DTOs.AppFeedbackDTOs;

public class AppFeedbackDto
{
    public Guid Id { get; set; }

    public string FullName { get; set; } = default!; // User.FirstName + LastName

    public string Message { get; set; } = default!;

    public DateTime SentAt { get; set; }

    public FeedbackType Type { get; set; }
}
