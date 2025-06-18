using AutoLife.Application.DTOs.CommonDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoLife.Application.DTOs.ServiceCentersDTOs;

public class CreateServiceCenterDto
{
    public string Name { get; set; } = default!;
    public string Phone { get; set; } = default!;
    public string ServiceType { get; set; } = default!;
    public GeoLocationDto Location { get; set; } = new();
}

