using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoLife.Application.DTOs.DistrictDTOs;

public class DistrictCreateDto
{
    public string UzName { get; set; } = string.Empty;
    public string RuName { get; set; } = string.Empty;
    public string EnName { get; set; } = string.Empty;
    public Guid RegionId { get; set; }
}
