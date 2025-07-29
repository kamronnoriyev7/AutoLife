using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoLife.Application.DTOs.UsersDTOs;

public class UserCreateDto
{
    public string FirstName { get; set; } = default!;
    public string LastName { get; set; } = default!;
    public string UserName { get; set; } = default!;
    public string PhoneNumber { get; set; } = default!;
    public string Email { get; set; } = default!;
    public DateOnly DateOfBirth { get; set; } = default!;
    public ICollection<IFormFile> ProfileImages { get; set; } = new List<IFormFile>();
    public Guid IdentityUserId { get; set; }

}
