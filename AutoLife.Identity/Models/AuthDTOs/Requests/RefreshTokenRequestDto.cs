using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoLife.Identity.Models.AuthDTOs.Requests;

public class RefreshTokenRequestDto
{
    public string RefreshToken { get; set; } = default!;
    public string AccessToken { get; set; } = default!;
}
