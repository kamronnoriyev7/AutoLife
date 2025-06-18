using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoLife.Application.DTOs.BookingDTOs;

public class CancelBookingParkingDto
{
    public Guid BookingId { get; set; }
    public string? Reason { get; set; } // Ixtiyoriy sababi
}

