using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoLife.Domain.Entities;

public class Image : BaseEntity
{
    public long Id { get; set; }

    public string? ImageUrl { get; set; } = default!;

    public long? VehicleId { get; set; }
    public Vehicle? Vehicle { get; set; }

    public long? UserId { get; set; }
    public User? User { get; set; }

    public long? FuelStationId { get; set; }
    public FuelStation? FuelStation { get; set; }

    public long? ParkingId { get; set; }
    public Parking? Parking { get; set; }

    public long? ServiceCenterId { get; set; }
    public ServiceCenter? ServiceCenter { get; set; }

    public long? NewsId { get; set; }
    public News? News { get; set; }

    public long? CompanyId { get; set; }
    public Company? Company { get; set; } // Agar Image kompaniyaga tegishli bo'lsa
}

