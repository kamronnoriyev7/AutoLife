using AutoLife.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoLife.Application.DTOs.BookingDTOs;

public class UpdateBookingStatusParkingDto
{
    public Guid BookingId { get; set; }
    public BookingStatus Status { get; set; }
}

