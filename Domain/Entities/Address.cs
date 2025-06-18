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
    public GeoLocation? Location { get; set; }  
    public string? Orientation { get; set; }

    [ForeignKey(nameof(Country))]
    public Guid CountryId { get; set; }
    public Country? Country { get; set; }

    [ForeignKey(nameof(Region))]
    public Guid? RegionId { get; set; }
    public Region? Region { get; set; }

    [ForeignKey(nameof(District))]
    public Guid? DistrictId { get; set; }
    public District? District { get; set; }
}
