using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoLife.Domain.Entities;

public class Company : BaseEntity
{
    public Guid Id { get; set; }  
    public string Name { get; set; } = default!; 
    public string? Description { get; set; } 
    public string? PhoneNumber { get; set; } 
    public string? Email { get; set; } 
    public string? Website { get; set; } 
    public string? LogoUrl { get; set; }

    public Guid? UserId { get; set; } 
    public User? User { get; set; } 

    public Address? Address { get; set; } 

    public ICollection<Rating>? Ratings { get; set; } = new List<Rating>();
    public ICollection<Notification>? Notifications { get; set; } = new List<Notification>(); 
    public ICollection<News>? NewsList { get; set; } = new List<News>();
    public ICollection<Parking>? Parkings { get; set; } = new List<Parking>(); 
    public ICollection<Vehicle>? Vehicles { get; set; } = new List<Vehicle>(); 
    public ICollection<FuelStation>? FuelStations { get; set; } = new List<FuelStation>(); 
    public ICollection<ServiceCenter>? ServiceCenters { get; set; } = new List<ServiceCenter>(); 
    public ICollection<Image>? Images { get; set; } = new List<Image>(); 
}
