using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoLife.Domain.Entities;

public class Parking : BaseEntity
{
    public long Id { get; set; }
    public string Name { get; set; } = default!;
    public string PhoneNumber { get; set; } = default!;
    public Address Address { get; set; } = new();
    public decimal HourlyRate { get; set; }
    public decimal DailyRate { get; set; }
    public bool IsFree { get; set; }
    public bool HasCameras { get; set; }
    public bool IsCovered { get; set; }

    public int TotalSpaces { get; set; }
    public int AvailableSpaces { get; set; }

    public string OpeningTime { get; set; } = default!;  // masalan: "08:00"
    public string ClosingTime { get; set; } = default!;  // masalan: "23:00"

    public bool IsPreBookingAllowed { get; set; } = true;

    public List<Image> Images { get; set; } = new();

    public double AverageRating { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}

