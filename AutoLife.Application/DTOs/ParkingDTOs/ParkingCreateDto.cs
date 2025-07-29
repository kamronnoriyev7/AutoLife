using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoLife.Application.DTOs.ParkingDTOs;

public class ParkingCreateDto
{
    public string Name { get; set; } = default!;

    public string PhoneNumber { get; set; } = default!;

    public Guid AddressId { get; set; }

    public Guid? UserId { get; set; }

    public decimal HourlyRate { get; set; }

    public decimal DailyRate { get; set; }

    public bool IsFree { get; set; }

    public bool HasCameras { get; set; }

    public bool IsCovered { get; set; }

    public int TotalSpaces { get; set; }

    public int AvailableSpaces { get; set; }

    public string OpeningTime { get; set; } = default!;

    public string ClosingTime { get; set; } = default!;

    public bool? IsPreBookingAllowed { get; set; }

    public Guid? CompanyId { get; set; }
}
