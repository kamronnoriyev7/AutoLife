using AutoLife.Application.DTOs.CommonDTOs;
using AutoLife.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoLife.Application.DTOs.ServiceCentersDTOs;

public class CreateServiceCenterDto
{
    public string Name { get; set; } = default!;
    public string? Description { get; set; }
    public string? PhoneNumber { get; set; }

    public Guid? UserId { get; set; }
    public Guid? AddressId { get; set; }
    public Guid? CompanyId { get; set; }

    public ServiceType ServiceType { get; set; }
}

