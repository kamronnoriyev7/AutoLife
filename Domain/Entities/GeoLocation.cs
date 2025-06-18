using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoLife.Domain.Entities;

public class GeoLocation : BaseEntity   
{
    public double Latitude { get; set; }
    public double Longitude { get; set; }

    public GeoLocation() { }

    public GeoLocation(double latitude, double longitude)
    {
        Latitude = latitude;
        Longitude = longitude;
    }
}
