using AutoLife.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoLife.Application.DTOs.RegionDTOs;

public class RegionResponseDto
{
    public Guid Id { get; set; }
    public string UzName { get; set; } = string.Empty;
    public string RuName { get; set; } = string.Empty;
    public string EnName { get; set; } = string.Empty;
    public Guid CountryId { get; set; }
    public string? CountryName { get; set; }

    public List<District>? Districts { get; set; }
}