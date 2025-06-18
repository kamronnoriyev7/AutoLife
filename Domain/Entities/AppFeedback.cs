using AutoLife.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoLife.Domain.Entities;

public class AppFeedback : BaseEntity
{
    public long Id { get; set; }

    public long UserId { get; set; }
    public User User { get; set; } = default!;

    public string Message { get; set; } = default!;
    public DateTime SentAt { get; set; } = DateTime.UtcNow;

    public FeedbackType Type { get; set; } = FeedbackType.Suggestion;
}

