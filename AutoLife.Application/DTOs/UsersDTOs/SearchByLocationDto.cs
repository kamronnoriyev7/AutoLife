﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoLife.Application.DTOs.UsersDTOs;

public class SearchByLocationDto
{
    public double Latitude { get; set; }
    public double Longitude { get; set; }
    public double RadiusInKm { get; set; } = 10;
}

