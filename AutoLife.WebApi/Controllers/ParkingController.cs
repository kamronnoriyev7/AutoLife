using AutoLife.Application.DTOs.ParkingDTOs;
using AutoLife.Domain.Entities;
using AutoLife.Infrastructure.Services.ParkingServices;
using Microsoft.AspNetCore.Mvc;

namespace AutoLife.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ParkingController : ControllerBase
{
    private readonly IParkingService _parkingService;

    public ParkingController(IParkingService parkingService)
    {
        _parkingService = parkingService;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] ParkingCreateDto parkingDto)
    {
        await _parkingService.AddParkingAsync(parkingDto);
        return Ok("Parking muvaffaqiyatli yaratildi.");
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var parkings = await _parkingService.GetAllParkingsAsync();
        return Ok(parkings);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var parking = await _parkingService.GetParkingByIdAsync(id);
        return Ok(parking);
    }

    [HttpGet("by-location")]
    public async Task<IActionResult> GetByLocation([FromQuery] string countryUzName)
    {
        var country = new Country { UzName = countryUzName };
        var parkings = await _parkingService.GetParkingsByLocationAsync(country);
        return Ok(parkings);
    }

    [HttpGet("by-type")]
    public async Task<IActionResult> GetByType([FromQuery] bool isFree)
    {
        var parkings = await _parkingService.GetParkingsByTypeAsync(isFree);
        return Ok(parkings);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] ParkingCreateDto parkingDto)
    {
        await _parkingService.UpdateParkingAsync(id, parkingDto);
        return Ok("Parking muvaffaqiyatli yangilandi.");
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _parkingService.DeleteParkingAsync(id);
        return Ok("Parking o'chirildi (soft delete).");
    }
}
