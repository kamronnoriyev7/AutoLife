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

    public FuelType FuelType { get; set; }
    public decimal Price { get; set; }
    public DateTime Date { get; set; } = DateTime.UtcNow;
}