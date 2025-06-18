using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoLife.Application.DTOs.BookingDTOs;

public class BookingResponseParkingDto
{
    public Guid Id { get; set; }

    public Guid ParkingId { get; set; }
    public string ParkingName { get; set; } = default!;

    public Guid UserId { get; set; }
    public string UserFullName { get; set; } = default!;

    public DateTime From { get; set; }
    public DateTime To { get; set; }
    public int SpotCount { get; set; }

    public decimal TotalPrice { get; set; }

    public string Status { get; set; } = default!;
    public DateTime CreatedAt { get; set; }
}

