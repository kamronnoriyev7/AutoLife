using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoLife.Domain.Entities;

public class News : BaseEntity
{
    public long Id { get; set; }
    public string? Title { get; set; } = default!;
    public string? Body { get; set; } = default!;

    public long? UserId { get; set; }
    public User? User { get; set; }

    public long? CompanyId { get; set; }
    public Company? Company { get; set; } // Agar yangilik kompaniyaga tegishli bo'lsa    

    public long? FuelStationId { get; set; } 
    public FuelStation? FuelStation { get; set; } 

    public long? ServiceCenterId { get; set; } 
    public ServiceCenter? ServiceCenter { get; set; }  

    public long? ParkingId { get; set; }
    public Parking? Parking { get; set; }

    public ICollection<Image>? Images { get; set; }

    public long? VehicleId { get; set; }
    public Vehicle? Vehicle { get; set; } // Agar yangilik transport vositasiga tegishli bo'lsa

}

