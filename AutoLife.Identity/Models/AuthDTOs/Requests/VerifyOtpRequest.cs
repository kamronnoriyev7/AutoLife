using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoLife.Identity.Models.AuthDTOs.Requests;

public class VerifyOtpRequest
{
    public string Email { get; set; } = default!;
    public string Code { get; set; } = default!;
}