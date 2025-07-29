using AutoLife.Application.DTOs.CommonDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoLife.Application.DTOs.FuelStationsDTOs;

public class FuelStationResponseDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = default!;
    public string? OperatorName { get; set; }
    public string? PhoneNumber { get; set; }
    public Guid FuelTypeId { get; set; }
    public Guid? FuelSubTypeId { get; set; }
    public Guid AddressId { get; set; }
    public Guid? UserId { get; set; }
    public Guid? CompanyId { get; set; }
}
