using AutoLife.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoLife.Domain.Entities;

public class Booking : BaseEntity            /// Booking ni umumiy barcha zakazlar uchun ishlatiladigan model qilish  
{
    public long Id { get; set; }
    public long UserId { get; set; }
    public User User { get; set; } = default!;
    public long ParkingId { get; set; }

    public DateTime From { get; set; }
    public DateTime To { get; set; }

    public int SpotCount { get; set; } 

    public decimal TotalPrice { get; set; }

    public BookingStatus Status { get; set; } 

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}

