using AutoLife.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoLife.Application.DTOs.BookingDTOs;

public class BookingResponseDto
{
    public Guid Id { get; set; }
    public string UserFullName { get; set; } = default!;
    public BookingType BookingType { get; set; }
    public Guid TargetId { get; set; }

    public string? TargetName { get; set; }
    public string? VehicleName { get; set; }

    public string Description { get; set; } = string.Empty;
    public string? AddressName { get; set; }

    public DateTime From { get; set; }
    public DateTime To { get; set; }
    public int? SpotCount { get; set; }
    public decimal TotalPrice { get; set; }
    public BookingStatus? Status { get; set; }
}
