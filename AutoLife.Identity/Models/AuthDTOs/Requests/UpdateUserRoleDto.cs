using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoLife.Identity.Models.AuthDTOs.Requests;

public class UpdateUserRoleDto
{
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; } = null;
}
