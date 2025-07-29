using AutoLife.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoLife.Application.DTOs.CompanyDTOs;

public class CompanyResponseDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = default!;
    public string? Description { get; set; }
    public string? PhoneNumber { get; set; }
    public string? Email { get; set; }
    public Address? Address { get; set; } 
    public string? Website { get; set; }
    public string? LogoUrl { get; set; }
    public Guid? UserId { get; set; }
}
