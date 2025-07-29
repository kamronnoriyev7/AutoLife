using AutoLife.Application.DTOs.CommonDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoLife.Application.DTOs.AddressDTOs;

public class AddressResponseDto
{
    public Guid Id { get; set; }
    public string? Orientation { get; set; }
    public string? Street { get; set; }
    public string? HouseNumber { get; set; }
    public GeoLocationDto? GeoLocation { get; set; }

    public Guid? UserId { get; set; }
    public string? UserFullName { get; set; }

    public Guid? CountryId { get; set; }
    public string? CountryName { get; set; }

    public Guid? RegionId { get; set; }
    public string? RegionName { get; set; }

    public Guid? DistrictId { get; set; }
    public string? DistrictName { get; set; }

    public Guid? CompanyId { get; set; }
    public string? CompanyName { get; set; }
}
