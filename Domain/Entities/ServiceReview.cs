using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoLife.Domain.Entities;

public class ServiceReview : BaseEntity
{
    public long Id { get; set; }

    public long ServiceCenterId { get; set; }
    public ServiceCenter ServiceCenter { get; set; } = default!;

    public long UserId { get; set; }
    public User User { get; set; } = default!;

    public int Rating { get; set; } // 1-5
    public string? Comment { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
