using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoLife.Domain.Entities;

public class Company : BaseEntity
{
    public long Id { get; set; } = 0; // Default value for Id, will be set by the database
    public string Name { get; set; } = default!; // Company name
    public string? Description { get; set; } // Optional description of the company
    public string? PhoneNumber { get; set; } // Optional phone number for the company
    public string? Email { get; set; } // Optional email address for the company
    public string? Website { get; set; } // Optional website URL for the company
    public string? LogoUrl { get; set; }

    public long? OwnerId { get; set; }
    public User? OwnerUser { get; set; }

    public long? UserId { get; set; } // Foreign key to the User entity
    public User? User { get; set; } // Navigation property to the User entity

    public ICollection<Rating>? Ratings { get; set; } = new List<Rating>(); // Collection of ratings associated with the company
    public ICollection<Notification>? Notifications { get; set; } = new List<Notification>(); // Collection of notifications associated with the company
    public ICollection<News>? NewsList { get; set; } = new List<News>(); // Collection of news articles associated with the company
    public ICollection<Parking>? Parkings { get; set; } = new List<Parking>(); // Collection of parkings associated with the company
    public ICollection<Vehicle>? Vehicles { get; set; } = new List<Vehicle>(); // Collection of vehicles associated with the company
    public ICollection<FuelStation>? FuelStations { get; set; } = new List<FuelStation>(); // Collection of fuel stations associated with the company
    public ICollection<ServiceCenter>? ServiceCenters { get; set; } = new List<ServiceCenter>(); // Collection of service centers associated with the company
    public ICollection<Address>? Addresses { get; set; } = new List<Address>(); // Collection of addresses associated with the company
    public ICollection<Image>? Images { get; set; } = new List<Image>(); // Collection of images associated with the company
}
