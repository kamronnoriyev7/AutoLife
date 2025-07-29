using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoLife.Domain.Entities;

public class Address : BaseEntity
{
    public Guid Id { get; set; } // Default value for Id, will be set by the database
    public string? Orientation { get; set; }
    public string? Street { get; set; }
    public string? HouseNumber { get; set; }
    public GeoLocation? GeoLocation { get; set; } 

    public Guid? UserId{ get; set; }
    public User? User { get; set; }

    public Guid? CountryId { get; set; }
    public Country? Country { get; set; }

    public Guid? RegionId { get; set; }
    public Region? Region { get; set; }

    public Guid? DistrictId { get; set; }
    public District? District { get; set; }

    public Guid? CompanyId { get; set; }
    public Company? Company { get; set; } 

    public ICollection<Parking>? Parkings { get; set; } = new List<Parking>();
    public ICollection<ServiceCenter>? ServiceCenters { get; set; } = new List<ServiceCenter>();
    public ICollection<FuelStation>? FuelStations { get; set; } = new List<FuelStation>();
}
