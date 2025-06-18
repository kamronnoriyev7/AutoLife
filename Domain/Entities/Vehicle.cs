using AutoLife.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoLife.Domain.Entities;

public class Vehicle : BaseEntity
{
    public long Id { get; set; }

    public long UserId { get; set; }
    public User User { get; set; } = default!;

    public string Brand { get; set; } = default!;
    public string Model { get; set; } = default!;
    public string NumberPlate { get; set; } = default!;
    public FuelType FuelType { get; set; }
}
