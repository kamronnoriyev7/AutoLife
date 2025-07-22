using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoLife.Domain.Entities;

public class Image : BaseEntity
{
    public Guid Id { get; set; }

    public string? ImageUrl { get; set; } = default!;

    public Guid? VehicleId { get; set; }
    public Vehicle? Vehicle { get; set; }

    public Guid? UserId { get; set; }
    public User? User { get; set; }

    public Guid? FuelStationId { get; set; }
    public FuelStation? FuelStation { get; set; }

    public Guid? ParkingId { get; set; }
    public Parking? Parking { get; set; }

    public Guid? ServiceCenterId { get; set; }
    public ServiceCenter? ServiceCenter { get; set; }

    public Guid? NewsId { get; set; }
    public News? News { get; set; }

    public Guid? CompanyId { get; set; }
    public Company? Company { get; set; } // Agar Image kompaniyaga tegishli bo'lsa
}

