using AutoLife.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoLife.Domain.Entities;

public class FuelSubType : BaseEntity
{
    public Guid Id { get; set; }
    public string Name { get; set; } = default!;
    public string Description { get; set; } = default!;

    public Guid FuelTypeId { get; set; }
    public FuelType FuelType { get; set; } = default!;
    public ICollection<FuelPrice> FuelPrices { get; set; } = new List<FuelPrice>();

}
