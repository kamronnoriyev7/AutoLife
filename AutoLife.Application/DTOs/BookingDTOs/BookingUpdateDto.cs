using AutoLife.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoLife.Application.DTOs.BookingDTOs;

public class BookingUpdateDto
{
    public string Description { get; set; } = string.Empty;
    public DateTime From { get; set; }
    public DateTime To { get; set; }
    public int? SpotCount { get; set; }
    public decimal TotalPrice { get; set; }
    public BookingStatus? Status { get; set; }
}
