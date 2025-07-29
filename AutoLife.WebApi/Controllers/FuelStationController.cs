using AutoLife.Application.DTOs.FuelStationsDTOs;
using AutoLife.Domain.Entities;
using AutoLife.Infrastructure.Services.FuelStationServices;
using Microsoft.AspNetCore.Mvc;

namespace AutoLife.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class FuelStationController : ControllerBase
{
    private readonly IFuelStationService _fuelStationService;

    public FuelStationController(IFuelStationService fuelStationService)
    {
        _fuelStationService = fuelStationService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllAsync()
    {
        var result = await _fuelStationService.GetAllFuelStationsAsync();
        return Ok(result);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetByIdAsync(Guid id)
    {
        var result = await _fuelStationService.GetFuelStationByIdAsync(id);
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> CreateAsync([FromBody] CreateFuelStationDto dto)
    {
        await _fuelStationService.AddFuelStationAsync(dto);
        return Ok("Yoqilg'i stansiyasi muvaffaqiyatli yaratildi.");
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdateAsync(Guid id, [FromBody] UpdateFuelStationDto dto)
    {
        var result = await _fuelStationService.UpdateFuelStationAsync(id, dto);
        return Ok(result);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteAsync(Guid id)
    {
        await _fuelStationService.DeleteFuelStationAsync(id);
        return Ok("Yoqilg'i stansiyasi o'chirildi.");
    }

    [HttpGet("by-fuel-type/{fuelTypeId:guid}")]
    public async Task<IActionResult> GetByFuelType(Guid fuelTypeId)
    {
        var fuelType = new Domain.Entities.FuelType { Id = fuelTypeId };
        var result = await _fuelStationService.GetFuelStationsByFuelTypeAsync(fuelType);
        return Ok(result);
    }

    [HttpGet("by-location")]
    public async Task<IActionResult> GetByLocation([FromQuery] double latitude, [FromQuery] double longitude)
    {
        var location = new GeoLocation
        {
            Latitude = latitude,
            Longitude = longitude
        };

        var result = await _fuelStationService.GetFuelStationsByLocationAsync(location);
        return Ok(result);
    }
}
