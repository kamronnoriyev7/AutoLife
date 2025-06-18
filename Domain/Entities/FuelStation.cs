using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoLife.Domain.Entities;

public class FuelStation : BaseEntity
{
    public long Id { get; set; }
    public string Name { get; set; } = default!;
    public Address Address { get; set; } = new ();
    public string? OperatorName { get; set; }
    public string? PhoneNumber { get; set; }
    public ICollection<Image> Images { get; set; } = new List<Image>();
    public ICollection<FuelPrice> FuelPrices { get; set; } = new List<FuelPrice>();
    public ICollection<Rating> Ratings { get; set; } = new List<Rating>();
}