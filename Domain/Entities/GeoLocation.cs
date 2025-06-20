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
    public long? AddressId { get; set; } // Agar geojoy manzil bilan bog'liq bo'lsa
    public Address? Address { get; set; } // Manzil bilan bog'lanish

    public GeoLocation() { }

    public GeoLocation(double latitude, double longitude)
    {
        Latitude = latitude;
        Longitude = longitude;
    }
}
