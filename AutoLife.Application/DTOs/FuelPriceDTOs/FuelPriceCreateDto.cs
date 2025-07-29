using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoLife.Application.DTOs.FuelPriceDTOs;

public class FuelPriceCreateDto
{
    public Guid FuelStationId { get; set; }
    public Guid FuelSubTypeId { get; set; }
    public decimal Price { get; set; }
    public DateOnly Date { get; set; }
}
