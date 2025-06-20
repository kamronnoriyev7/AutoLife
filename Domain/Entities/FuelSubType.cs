using AutoLife.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoLife.Domain.Entities;

public class FuelSubType : BaseEntity
{
    public long Id { get; set; }
    public string Name { get; set; } = default!;  // Masalan: "A-92", "A-95", "Diesel"
    public FuelPrice FuelPrice { get; set; } = default!;  // FuelPrice bilan bog'liq bo'lishi kerak
    public string Description { get; set; } = default!;  // Masalan: "A-92 benzini, standart benzin turi"
    public FuelType FuelType { get; set; }  // FuelType enumidan foydalaniladi

}
