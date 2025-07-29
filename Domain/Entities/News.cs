using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoLife.Domain.Entities;

public class News : BaseEntity
{
    public Guid Id { get; set; }
    public string? Title { get; set; } = default!;
    public string? Body { get; set; } = default!;

    public Guid? UserId { get; set; }
    public User? User { get; set; }

    public Guid? CompanyId { get; set; }
    public Company? Company { get; set; }   

    public Guid? FuelStationId { get; set; } 
    public FuelStation? FuelStation { get; set; } 

    public Guid? ServiceCenterId { get; set; } 
    public ServiceCenter? ServiceCenter { get; set; }  

    public Guid? ParkingId { get; set; }
    public Parking? Parking { get; set; }

    public ICollection<Image>? Images { get; set; }

    public Guid? VehicleId { get; set; }
    public Vehicle? Vehicle { get; set; } 

}

