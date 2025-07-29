using AutoLife.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoLife.Domain.Entities;

public class Vehicle : BaseEntity
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public User User { get; set; } = default!;
    public ICollection<Image>? Images { get; set; } = null; 
    public string Brand { get; set; } = default!;
    public string Model { get; set; } = default!;
    public string NumberPlate { get; set; } = default!;
    public Guid FuelTypeId { get; set; } 
    public FuelType? FuelType { get; set; }  

    public ICollection<Booking>? Bookings { get; set; } = new List<Booking>(); 
    public ICollection<Rating>? Ratings { get; set; } = new List<Rating>(); 
    public ICollection<News>? News { get; set; } = new List<News>(); 
    public ICollection<Favorite>? Favorites { get; set; } = new List<Favorite>(); 
    public ICollection<Notification>? Notifications { get; set; }

}
