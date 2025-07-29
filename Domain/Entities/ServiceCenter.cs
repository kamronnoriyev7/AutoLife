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

    public Guid? UserId { get; set; } 
    public User? User { get; set; } = null; 

    public ServiceType ServiceType { get; set; }
    public Guid? AddressId { get; set; }
    public Address? Address { get; set; } = new(); 
    public Guid? CompanyId { get; set; }
    public Company? Company { get; set; } = null;

    public ICollection<Image> Images { get; set; } = new List<Image>();
    public ICollection<Rating> Ratings { get; set; } = new List<Rating>();
    public ICollection<News>? News { get; set; } = new List<News>();
    public ICollection<Favorite>? Favorites { get; set; } = new List<Favorite>(); 
    public ICollection<Notification>? Notifications { get; set; } = new List<Notification>(); 
    public ICollection<Booking>? Bookings { get; set; } = new List<Booking>(); 
}
