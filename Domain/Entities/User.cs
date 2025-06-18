using AutoLife.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoLife.Domain.Entities;

public class User : BaseEntity
{
    public long Id { get; set; }
    public string FirstName { get; set; } = default!;
    public string LastName { get; set; } = default!;
    public string UserName { get; set; } = default!;
    public string PhoneNumber { get; set; } = default!;
    public string Email { get; set; } = default!;
    public string? PasswordHash { get; set; } = default!;
    public string? PasswordSalt { get; set; } = default!;
    public ICollection<Vehicle>? Vehicles { get; set; } = null; 
    public UserRole Role { get; set; } 
    public Image? ProfileImage { get; set; } = null;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateOnly DateOfBirth { get; set; } = default!;
    public ICollection<Booking> Bookings { get; set; } = new List<Booking>();
    public ICollection<Notification> Notifications { get; set; } = new List<Notification>();
    public ICollection<Rating> Ratings { get; set; } = new List<Rating>();
    public ICollection<RefreshToken> RefreshTokens { get; set; } = new List<RefreshToken>();
    public ICollection<AppFeedback> AppFeedbacks { get; set; } = new List<AppFeedback>();
}