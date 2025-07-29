using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoLife.Domain.Entities;

public class Parking : BaseEntity
{
    public Guid Id { get; set; }
    public string Name { get; set; } = default!;
    public string PhoneNumber { get; set; } = default!;
    public Guid AddressId { get; set; }
    public Address Address { get; set; } = new();

    public Guid? UserId { get; set; } 
    public User? User { get; set; } = null; 

    public decimal HourlyRate { get; set; }
    public decimal DailyRate { get; set; }
    public bool IsFree { get; set; }
    public bool HasCameras { get; set; }
    public bool IsCovered { get; set; }
    public int TotalSpaces { get; set; }
    public int AvailableSpaces { get; set; }

    public string OpeningTime { get; set; } = default!;
    public string ClosingTime { get; set; } = default!;  

    public bool? IsPreBookingAllowed { get; set; } 

    public double AverageRating { get; set; }

    public Guid? CompanyId { get; set; }
    public Company? Company { get; set; } = null; 

    public ICollection<Rating> Ratings { get; set; } = new List<Rating>();
    public ICollection<Image>? Images { get; set; }
    public ICollection<News>? News { get; set; } = new List<News>();
    public ICollection<Booking>? Bookings { get; set; } = new List<Booking>(); 
    public ICollection<Favorite>? Favorites { get; set; } = new List<Favorite>(); 
    public ICollection<Notification>? Notifications { get; set; } = new List<Notification>();
}

