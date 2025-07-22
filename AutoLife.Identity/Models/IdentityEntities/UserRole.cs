using AutoLife.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoLife.Identity.Models.IdentityEntities;

public class UserRole : BaseEntity
{
    public Guid Id { get; set; }
    public string Name { get; set; } = default!; // Masalan: Admin, User, Manager
    public string? Description { get; set; } 

    public ICollection<IdentityUser> Users { get; set; } = new List<IdentityUser>();
}
