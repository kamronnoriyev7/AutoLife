using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoLife.Domain.Entities;

public class GeoLocation : BaseEntity   
{
    public long Id { get; set; } // Geojoyning ID
    public double Latitude { get; set; }
    public double Longitude { get; set; }
    public string? Description { get; set; } // Qo'shimcha ma'lumotlar uchun
    public long AddressId { get; set; }  // ✅ foreign key shu yerda!
    public Address Address { get; set; } = default!;

}
