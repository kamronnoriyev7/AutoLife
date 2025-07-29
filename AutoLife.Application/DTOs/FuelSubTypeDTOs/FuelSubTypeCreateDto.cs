using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoLife.Application.DTOs.FuelSubTypeDTOs;

public class FuelSubTypeCreateDto
{
    public string Name { get; set; } = default!;
    public string Description { get; set; } = default!;
    public Guid FuelTypeId { get; set; }
}