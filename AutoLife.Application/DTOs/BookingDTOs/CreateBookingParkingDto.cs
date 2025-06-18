using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoLife.Application.DTOs.BookingDTOs;

public class CreateBookingParkingDto
{
    public Guid Id { get; set; } // Booking ID
    public Guid UserId { get; set; } // Foydalanuvchi ID
    public Guid ParkingId { get; set; }

    public DateTime From { get; set; } // Boshlanish vaqti
    public DateTime To { get; set; }   // Tugash vaqti

    public int SpotCount { get; set; } = 1; // Nechta joy kerak
}

