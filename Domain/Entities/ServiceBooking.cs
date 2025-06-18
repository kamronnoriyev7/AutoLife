using AutoLife.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoLife.Domain.Entities;

public class ServiceBooking : BaseEntity
{
    public long Id { get; set; }

    public long UserId { get; set; }
    public User User { get; set; } = default!;

    public long ServiceCenterId { get; set; }
    public ServiceCenter ServiceCenter { get; set; } = default!;

    public DateTime BookingDate { get; set; }
    public string? Comment { get; set; }

    public BookingStatus Status { get; set; } = BookingStatus.Pending;
}