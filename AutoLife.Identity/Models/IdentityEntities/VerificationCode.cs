using AutoLife.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoLife.Identity.Models.IdentityEntities;

public class VerificationCode : BaseEntity
{
    public Guid Id { get; set; }
    public string? Email { get; set; } 
    public string? Code { get; set; }
    public DateTime ExpireAt { get; set; }
    public bool IsUsed { get; set; } = false;
    public bool IsVerified { get; set; } = false; 
} 
