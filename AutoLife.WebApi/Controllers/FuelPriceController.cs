using AutoLife.Application.DTOs.FuelPriceDTOs;
using AutoLife.Domain.Entities;
using AutoLife.Infrastructure.Services.FuelPriceServices;
using Microsoft.AspNetCore.Mvc;

namespace AutoLife.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class FuelPriceController : ControllerBase
{
    private readonly IFuelPriceService _fuelPriceService;

    public FuelPriceController(IFuelPriceService fuelPriceService)
    {
        _fuelPriceService = fuelPriceService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<FuelPrice>>> GetAllAsync()
    {
        var prices = await _fuelPriceService.GetAllFuelPricesAsync();
        return Ok(prices);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<FuelPrice>> GetByIdAsync(Guid id)
    {
        var price = await _fuelPriceService.GetFuelPriceByIdAsync(id);
        return Ok(price);
    }


    [HttpGet("subtype/{fuelSubTypeId}")]
    public async Task<ActionResult<IEnumerable<FuelPrice>>> GetByFuelSubTypeId(Guid fuelSubTypeId)
    {
        var prices = await _fuelPriceService.GetFuelPricesByFuelSubTypeIdAsync(fuelSubTypeId);
        return Ok(prices);
    }

    [HttpPost]
    public async Task<ActionResult<FuelPrice>> CreateAsync([FromBody] FuelPriceCreateDto dto)
    {
        var created = await _fuelPriceService.AddFuelPriceAsync(dto);
        return CreatedAtAction(nameof(GetByIdAsync), new { id = created.Id }, created);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<FuelPrice>> UpdateAsync(Guid id, [FromBody] FuelPriceCreateDto dto)
    {
        var updated = await _fuelPriceService.UpdateFuelPriceAsync(id, dto);
        return Ok(updated);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync(Guid id)
    {
        await _fuelPriceService.DeleteFuelPriceAsync(id);
        return NoContent();
    }
}
