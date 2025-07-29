using AutoLife.Application.DTOs.VehicleDTOs;
using AutoLife.Infrastructure.Services.VehicleServices;
using Microsoft.AspNetCore.Mvc;

namespace AutoLife.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class VehicleController : ControllerBase
{
    private readonly IVehicleService _vehicleService;

    public VehicleController(IVehicleService vehicleService)
    {
        _vehicleService = vehicleService;
    }

    // POST: api/vehicle
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] VehicleCreateDto vehicleDto)
    {
        await _vehicleService.AddVehicleAsync(vehicleDto);
        return Ok(new { message = "Vehicle created successfully." });
    }

    // GET: api/vehicle
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var vehicles = await _vehicleService.GetAllVehiclesAsync();
        return Ok(vehicles);
    }

    // GET: api/vehicle/{id}
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var vehicle = await _vehicleService.GetVehicleByIdAsync(id);
        return Ok(vehicle);
    }

    // GET: api/vehicle/by-user/{userId}
    [HttpGet("by-user/{userId}")]
    public async Task<IActionResult> GetByUserId(Guid userId)
    {
        var vehicles = await _vehicleService.GetVehiclesByUserIdAsync(userId);
        return Ok(vehicles);
    }

    // GET: api/vehicle/by-make/{make}
    [HttpGet("by-make/{make}")]
    public async Task<IActionResult> GetByMake(string make)
    {
        var vehicles = await _vehicleService.GetVehiclesByMakeAsync(make);
        return Ok(vehicles);
    }

    // GET: api/vehicle/by-model/{model}
    [HttpGet("by-model/{model}")]
    public async Task<IActionResult> GetByModel(string model)
    {
        var vehicles = await _vehicleService.GetVehiclesByModelAsync(model);
        return Ok(vehicles);
    }

    // PUT: api/vehicle/{id}
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] VehicleCreateDto vehicleDto)
    {
        await _vehicleService.UpdateVehicleAsync(id, vehicleDto);
        return Ok(new { message = "Vehicle updated successfully." });
    }

    // DELETE: api/vehicle/{id}
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _vehicleService.DeleteVehicleAsync(id);
        return Ok(new { message = "Vehicle deleted successfully (soft delete)." });
    }
}
