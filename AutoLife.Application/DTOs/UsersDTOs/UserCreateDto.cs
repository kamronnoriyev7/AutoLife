using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoLife.Application.DTOs.UsersDTOs;

public class UserCreateDto
{
    public Guid Id { get; set; }
    public string FirstName { get; set; } = default!;
    public string LastName { get; set; } = default!;
    public string UserName { get; set; } = default!;
    public string PhoneNumber { get; set; } = default!;
    public string Email { get; set; } = default!;
    public DateOnly DateOfBirth { get; set; } = default!;
    public bool IsActive { get; set; } = true;

    public Guid IdentityUserId { get; set; }

}
