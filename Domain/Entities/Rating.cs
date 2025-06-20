using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoLife.Domain.Entities;

public class Rating : BaseEntity
{
    public long Id { get; set; }

    public long? UserId { get; set; }
    public User? User { get; set; } = default!;

    public int? Stars { get; set; } // 1–5
    public string? Comment { get; set; }

    public long? ServiceCenterId { get; set; }
    public ServiceCenter? ServiceCenter { get; set; } = null; // If the rating is for a service center

    public long? ParkingId { get; set; }
    public Parking? Parking { get; set; } = null; // If the rating is for a parking

    public long? FuelStationId { get; set; }
    public FuelStation? FuelStation { get; set; } = default!;

    public long? Companyid { get; set; }
    public Company? Company { get; set; } = null; // If the rating is for a company

    public long? VehicleId { get; set; }
    public Vehicle? Vehicle { get; set; } = null; // If the rating is for a vehicle

}
