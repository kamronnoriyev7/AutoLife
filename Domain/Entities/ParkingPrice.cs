using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoLife.Domain.Entities;

public class ParkingPrice : BaseEntity
{
    public long Id { get; set; }

    public long ParkingId { get; set; }
    public Parking Parking { get; set; } = default!;

    public TimeSpan FromTime { get; set; }  // Masalan: 08:00
    public TimeSpan ToTime { get; set; }    // Masalan: 18:00

    public decimal PricePerHour { get; set; }

    public string? Description { get; set; } // Masalan: Kunduzgi narx
}
