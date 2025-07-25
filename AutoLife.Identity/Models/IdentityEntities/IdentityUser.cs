using AutoLife.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoLife.Identity.Models.IdentityEntities;

public class IdentityUser : BaseEntity
{
    public Guid Id { get; set; }

    public string UserName { get; set; } = default!;
    public string Email { get; set; } = default!;
    public string PhoneNumber { get; set; } = default!;

    public string PasswordSalt { get; set; } = default!;
    public string PasswordHash { get; set; } = default!;

    public bool IsActive { get; set; } = true;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public Guid RoleId { get; set; }
    public UserRole? Role { get; set; }

    public bool IsEmailConfirmed { get; set; } = false;
    public bool IsPhoneNumberConfirmed { get; set; } = false;

    public Guid UserId { get; set; } 
    public ICollection<RefreshToken> RefreshTokens { get; set; } = new List<RefreshToken>(); // Collection of refresh tokens associated with the user
}
