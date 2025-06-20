using AutoLife.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoLife.Domain.Entities;

public class FuelHistory : BaseEntity
{
    public long Id { get; set; }
    public long UserId { get; set; }
    public User User { get; set; } = default!;
    public long FuelStationId { get; set; }
    public FuelStation FuelStation { get; set; } = default!;
    public FuelType FuelType { get; set; }
    public decimal OldPrice { get; set; }
    public decimal NewPrice { get; set; }
    public DateTime ChangedAt { get; set; } = DateTime.UtcNow;
}
