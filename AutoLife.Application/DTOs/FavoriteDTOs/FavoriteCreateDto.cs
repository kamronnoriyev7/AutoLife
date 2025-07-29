using AutoLife.Domain.Entities;
using AutoLife.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoLife.Application.DTOs.FavoriteDTOs;

public class FavoriteCreateDto
{
    public Guid UserId { get; set; }
    public User User { get; set; } = default!;

    public FavoriteType Type { get; set; }

    public Guid? FuelStationId { get; set; }
    public FuelStation? FuelStation { get; set; } = null;

    public Guid? ParkingId { get; set; }
    public Parking? Parking { get; set; } = null;

    public Guid? VehicleId { get; set; }
    public Vehicle? Vehicle { get; set; } = null;

    public Guid? ServiceCenterId { get; set; }
    public ServiceCenter? ServiceCenter { get; set; } = null;
}
