using AutoLife.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoLife.Application.DTOs.BookingDTOs;

public class BookingFilterParkingDto
{
    public Guid? UserId { get; set; }
    public Guid? ParkingId { get; set; }

    public BookingStatus? Status { get; set; }

    public DateTime? FromDate { get; set; }
    public DateTime? ToDate { get; set; }
}

