using AutoLife.Domain.Enums;
using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoLife.Domain.Entities;

public class FuelPrice : BaseEntity
{
    public long Id { get; set; }
    public long FuelStationId { get; set; }
    public FuelStation FuelStation { get; set; } = default!;

    public FuelType? FuelType { get; set; }
    public long FuelSubTypeId { get; set; }
    public FuelSubType FuelSubType { get; set; } = default!;

    public decimal Price { get; set; }
    public DateOnly Date { get; set; } // Narxning amal qilish sanasi

}