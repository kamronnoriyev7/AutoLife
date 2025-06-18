using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoLife.Application.DTOs.AuthDTOs;

public class RefreshTokenRequestDto
{
    public string RefreshToken { get; set; } = default!;
}

