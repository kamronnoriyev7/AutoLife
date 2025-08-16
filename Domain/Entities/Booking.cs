using AutoLife.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoLife.Domain.Entities;

public class Booking : BaseEntity            /// Booking ni umumiy barcha zakazlar uchun ishlatiladigan model qilish  
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public User User { get; set; } = default!;

    public BookingType BookingType { get; set; } 
    public Guid TargetId { get; set; } 

    public Guid? VehicleId { get; set; }
    public Vehicle? Vehicle { get; set; } = default!;

    public string Description { get; set; } = string.Empty;

    public Guid? AddressId { get; set; } 
    public Address? Address { get; set; } = new(); 

    public DateTime From { get; set; }
    public DateTime To { get; set; }
    public int? SpotCount { get; set; } 
    public decimal TotalPrice { get; set; }
    public BookingStatus? Status { get; set; } 

    public ICollection<Notification>? Notifications { get; set; } //Har bir booking uchun notification bo'lishi kerak
    public ICollection<Rating>? Ratings { get; set; } // Har bir booking uchun rating bo'lishi kerak, lekin bu optional bo'lishi mumkin

}

