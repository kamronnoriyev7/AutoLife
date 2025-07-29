using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoLife.Application.DTOs.VehicleDTOs;

public class VehicleCreateDto 
{
    public Guid UserId { get; set; }
    public string Brand { get; set; } = default!;
    public string Model { get; set; } = default!;
    public string NumberPlate { get; set; } = default!;
    public Guid FuelTypeId { get; set; }
}
