using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoLife.Domain.Entities;

public class News : BaseEntity
{
    public long Id { get; set; }
    public string Title { get; set; } = default!;
    public string Body { get; set; } = default!;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public long? FuelStationId { get; set; } // optional
    public long? ServiceCenterId { get; set; } // optional
}

