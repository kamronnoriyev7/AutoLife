using AutoLife.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoLife.Domain.Entities;

public class AppFeedback : BaseEntity
{
    public Guid Id { get; set; }

    public Guid UserId { get; set; }
    public User User { get; set; } = default!;

    public string Message { get; set; } = default!;
    public DateTime SentAt { get; set; } = DateTime.UtcNow;

    public FeedbackType Type { get; set; } = FeedbackType.Suggestion;
}

