using AutoLife.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoLife.Domain.Entities;

public class Vehicle : BaseEntity
{
    public long Id { get; set; }
    public long UserId { get; set; }
    public User User { get; set; } = default!;
    public ICollection<Image>? Images { get; set; } = null; // Agar transport vositasi rasmga ega bo'lsa
    public string Brand { get; set; } = default!;
    public string Model { get; set; } = default!;
    public string NumberPlate { get; set; } = default!;
    public FuelType FuelType { get; set; }

    public ICollection<Booking>? Bookings { get; set; } = new List<Booking>(); // Collection of bookings associated with the vehicle
    public ICollection<Rating>? Ratings { get; set; } = new List<Rating>(); // Collection of ratings associated with the vehicle
    public ICollection<News>? News { get; set; } = new List<News>(); // Collection of news articles associated with the vehicle
    public ICollection<Favorite>? Favorites { get; set; } = new List<Favorite>(); // Collection of favorites associated with the vehicle
    public ICollection<Notification>? Notifications { get; set; } // Collection of notifications associated with the vehicle

}
