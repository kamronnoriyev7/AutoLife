using AutoLife.Application.DTOs.CommonDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoLife.Application.DTOs.AddressDTOs;

public class CreateAddressDto
{
    public string? Orientation { get; set; }
    public string? Street { get; set; }
    public string? HouseNumber { get; set; }
    public GeoLocationDto? GeoLocation { get; set; }

    public Guid? UserId { get; set; }
    public Guid? CountryId { get; set; }
    public Guid? RegionId { get; set; }
    public Guid? DistrictId { get; set; }
    public Guid? CompanyId { get; set; }
}
