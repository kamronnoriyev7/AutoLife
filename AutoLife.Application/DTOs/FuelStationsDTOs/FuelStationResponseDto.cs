using AutoLife.Application.DTOs.CommonDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoLife.Application.DTOs.FuelStationsDTOs;

public class FuelStationResponseDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = default!;
    public string Address { get; set; } = default!;
    public string Phone { get; set; } = default!;
    public GeoLocationDto Location { get; set; } = new();
    public double AverageRating { get; set; }
}
