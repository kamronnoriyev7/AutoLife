using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoLife.Domain.Entities;

public class Rating : BaseEntity
{
    public Guid Id { get; set; }

    public Guid? UserId { get; set; }
    public User? User { get; set; } = default!;

    public int? Stars { get; set; } // 1–5
    public string? Comment { get; set; }

    public Guid? ServiceCenterId { get; set; }
    public ServiceCenter? ServiceCenter { get; set; } = null; // If the rating is for a service center

    public Guid? ParkingId { get; set; }
    public Parking? Parking { get; set; } = null; // If the rating is for a parking

    public Guid? FuelStationId { get; set; }
    public FuelStation? FuelStation { get; set; } = default!;

    public Guid? Companyid { get; set; }
    public Company? Company { get; set; } = null; // If the rating is for a company

    public Guid? VehicleId { get; set; }
    public Vehicle? Vehicle { get; set; } = null; // If the rating is for a vehicle

}
