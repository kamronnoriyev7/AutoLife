using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoLife.Application.DTOs.UsersDTOs;

public class UserResponseDto
{
    public Guid Id { get; set; }
    public string FirstName { get; set; } = default!;
    public string LastName { get; set; } = default!;
    public string PhoneNumber { get; set; } = default!;
    public string FullName { get; set; } = default!;
    public string Email { get; set; } = default!;
    public string UserName { get; set; } = default!;
    public string? Token { get; set; } = null;
}

