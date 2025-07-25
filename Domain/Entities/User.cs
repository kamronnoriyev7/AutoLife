using AutoLife.Domain.Enums;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoLife.Domain.Entities;

public class User : BaseEntity
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

    public ICollection<Image>? Images { get; set; }  
    public ICollection<Vehicle>? Vehicles { get; set; } = null;
    public ICollection<Booking>? Bookings { get; set; } = new List<Booking>();
    public ICollection<Notification>? Notifications { get; set; } = new List<Notification>();
    public ICollection<Rating>? Ratings { get; set; } = new List<Rating>();
    public ICollection<AppFeedback>? AppFeedbacks { get; set; } = new List<AppFeedback>();
    public ICollection<Favorite>? Favorites { get; set; } = new List<Favorite>();
    public ICollection<FuelStation>? FuelStations { get; set; } = new List<FuelStation>();
    public ICollection<Parking>? Parkings { get; set; } = new List<Parking>();
    public ICollection<ServiceCenter>? ServiceCenters { get; set; } = new List<ServiceCenter>();
    public ICollection<Company>? Companies { get; set; } = new List<Company>(); // Companies the user is associated with, if any
    public ICollection<News>? News { get; set; } = new List<News>(); // News articles the user is associated with, if any
    public ICollection<Address>? Addresses { get; set; } = new List<Address>(); // Addresses the user is associated with, if any

}