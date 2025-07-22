using AutoLife.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoLife.Domain.Entities;

public class ServiceCenter : BaseEntity
{
    public Guid Id { get; set; }
    public string Name { get; set; } = default!;
    public string? Description { get; set; }
    public string? PhoneNumber { get; set; }

    public Guid? UserId { get; set; } // Foreign key to the User entity
    public User? User { get; set; } = null; // Nullable to allow for service centers not associated with a user

    public ServiceType ServiceType { get; set; }
    public Guid? AddressId { get; set; }
    public Address? Address { get; set; } = new(); // Nullable to allow for service centers without an address

    public Guid? CompanyId { get; set; }
    public Company? Company { get; set; } = null; // Nullable to allow for service centers not associated with a company

    public ICollection<Image> Images { get; set; } = new List<Image>();
    public ICollection<Rating> Ratings { get; set; } = new List<Rating>();
    public ICollection<News>? News { get; set; } = new List<News>();
    public ICollection<Favorite>? Favorites { get; set; } = new List<Favorite>(); // Collection of favorites associated with the service center
    public ICollection<Notification>? Notifications { get; set; } = new List<Notification>(); // Collection of notifications associated with the service center
    public ICollection<Booking>? Bookings { get; set; } = new List<Booking>(); // Collection of bookings associated with the service center
}
