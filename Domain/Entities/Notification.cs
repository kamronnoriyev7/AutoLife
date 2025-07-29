using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoLife.Domain.Entities;

public class Notification : BaseEntity
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public User User { get; set; } = default!;
    public string? Title { get; set; } = default!;
    public string? Body { get; set; } = default!;
    public bool IsRead { get; set; } = false;

    public Guid ? VehicleId { get; set; }
    public Vehicle? Vehicle { get; set; } = null; 

    public Guid ? ServiceCenterId { get; set; }
    public ServiceCenter? Service { get; set; } = null; 


    public Guid? BookingId { get; set; } = null; 
    public Booking? Booking { get; set; } = null;

    public Guid ? FuelStationId { get; set; } = null; 
    public FuelStation? FuelStation { get; set; } = null; 

    public Guid ? ParkingId { get; set; } = null; 
    public Parking? Parking { get; set; } = null;

    public Guid ? CompanyId { get; set; } = null; 
    public Company? Company { get; set; } = null; 

}
