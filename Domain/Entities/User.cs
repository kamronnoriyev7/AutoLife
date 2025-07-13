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
    public bool IsActive { get; set; } = true; // Indicates if the user account is active
    public bool isAdmin { get; set; } = false; // Indicates if the user has admin privileges

    public UserRole Role { get; set; } 
    public DateOnly DateOfBirth { get; set; } = default!;

    public ICollection<Image>? Images { get; set; }  // Collection of images associated with the user
    public ICollection<Vehicle>? Vehicles { get; set; } = null;
    public ICollection<Booking>? Bookings { get; set; } = new List<Booking>();
    public ICollection<Notification>? Notifications { get; set; } = new List<Notification>();
    public ICollection<Rating>? Ratings { get; set; } = new List<Rating>();
    public ICollection<RefreshToken>? RefreshTokens { get; set; } = new List<RefreshToken>();
    public ICollection<AppFeedback>? AppFeedbacks { get; set; } = new List<AppFeedback>();
    public ICollection<Favorite>? Favorites { get; set; } = new List<Favorite>();
    public ICollection<FuelStation>? FuelStations { get; set; } = new List<FuelStation>();
    public ICollection<Parking>? Parkings { get; set; } = new List<Parking>();
    public ICollection<ServiceCenter>? ServiceCenters { get; set; } = new List<ServiceCenter>();
    public ICollection<Company>? Companies { get; set; } = new List<Company>(); // Companies the user is associated with, if any
    public ICollection<News>? News { get; set; } = new List<News>(); // News articles the user is associated with, if any
    public ICollection<Address>? Addresses { get; set; } = new List<Address>(); // Addresses the user is associated with, if any

}