using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoLife.Identity.Models.AuthDTOs.Requests;

public class ChangePasswordRequestDto
{
    public string OldPassword { get; set; } = default!;
    public string NewPassword { get; set; } = default!;
}
