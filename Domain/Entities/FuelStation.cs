using AutoLife.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoLife.Domain.Entities;

public class FuelStation : BaseEntity
{
    public long Id { get; set; }
    public string Name { get; set; } = default!;
    public long? AddressId { get; set; } // Foreign key to the Address entity
    public Address? Address { get; set; } = new ();
    public string? OperatorName { get; set; }
    public string? PhoneNumber { get; set; }

    public long? UserId { get; set; } // Foreign key to the User entity
    public User? User { get; set; } = null; // Nullable to allow for fuel stations not associated with a user

    public FuelType? FuelType { get; set; } // Enum for fuel type (e.g., petrol, diesel, etc.)
    public FuelSubType? FuelSubType { get; set; } // Enum for fuel subtype (e.g., regular, premium, etc.)

    public long? CompanyId { get; set; } // Foreign key to the Company entity                                             
    public Company? Company { get; set; } = null; // Nullable to allow for fuel stations not associated with a company

    public ICollection<Image>? Images { get; set; } = new List<Image>();
    public ICollection<FuelPrice>? FuelPrices { get; set; } = new List<FuelPrice>();
    public ICollection<Rating>? Ratings { get; set; } = new List<Rating>();
    public ICollection<Favorite>? Favorites { get; set; } = new List<Favorite>();
    public ICollection<Notification>? Notifications { get; set; } = new List<Notification>(); // Collection of notifications associated with the fuel station
    public ICollection<FuelHistory>? FuelHistories { get; set; } = new List<FuelHistory>();
    public ICollection<News>? News { get; set; } = new List<News>(); // Collection of news articles associated with the fuel station
    public ICollection<Booking>? Bookings { get; set; } = new List<Booking>(); // Collection of bookings associated with the fuel station

}