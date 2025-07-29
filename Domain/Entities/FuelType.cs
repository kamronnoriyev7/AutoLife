using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoLife.Domain.Entities;

public class FuelType : BaseEntity
{
    public Guid Id { get; set; }
    public string Name { get; set; } = default!;
    public string Description { get; set; } = default!;
    public Guid FuelStationId { get; set; }
    public FuelStation FuelStation { get; set; } = default!;

    public ICollection<FuelSubType> FuelSubTypes { get; set; } = new List<FuelSubType>();
}
