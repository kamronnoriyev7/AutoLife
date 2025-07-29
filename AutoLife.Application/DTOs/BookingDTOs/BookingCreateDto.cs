using AutoLife.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoLife.Application.DTOs.BookingDTOs;

public class BookingCreateDto
{
    public Guid UserId { get; set; }
    public BookingType BookingType { get; set; }
    public Guid TargetId { get; set; }

    public Guid? VehicleId { get; set; }
    public string Description { get; set; } = string.Empty;

    public Guid? AddressId { get; set; }

    public DateTime From { get; set; }
    public DateTime To { get; set; }
    public int? SpotCount { get; set; }
    public decimal TotalPrice { get; set; }
}
