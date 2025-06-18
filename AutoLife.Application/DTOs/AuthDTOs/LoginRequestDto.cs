using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoLife.Application.DTOs.AuthDTOs;

public class LoginRequestDto
{
    public string? UserName { get; set; } = default!;
    public string? Email { get; set; } = default!;
    public string? PhoneNumber { get; set; } = default!;
    public string Password { get; set; } = default!;
}
