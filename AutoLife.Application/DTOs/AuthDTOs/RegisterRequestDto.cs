using AutoLife.Domain.Entities;
using AutoLife.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoLife.Application.DTOs.AuthDTOs;

public class RegisterRequestDto
{
    public string FirstName { get; set; } = default!;
    public string LastName { get; set; } = default!;
    public string UserName { get; set; } = default!;
    public UserRole Role { get; set; } 
    public string Email { get; set; } = default!;
    public string Password { get; set; } = default!;
    public string PhoneNumber { get; set; } = default!;
    public string? Address { get; set; } = default!;
    public Image? ImageUrl { get; set; } = default!;
    public DateTime? DateOfBirth { get; set; } = null;
}
