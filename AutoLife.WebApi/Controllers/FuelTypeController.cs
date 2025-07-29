using AutoLife.Application.DTOs.FuelTypeDTOs;
using AutoLife.Domain.Entities;
using AutoLife.Infrastructure.Services.FuelTypeServices;
using Microsoft.AspNetCore.Mvc;

namespace AutoLife.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class FuelTypeController : ControllerBase
{
    private readonly IFuelTypeService _fuelTypeService;

    public FuelTypeController(IFuelTypeService fuelTypeService)
    {
        _fuelTypeService = fuelTypeService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<FuelType>>> GetAll()
    {
        var result = await _fuelTypeService.GetAllFuelTypesAsync();
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<FuelType>> GetById(Guid id)
    {
        try
        {
            var fuelType = await _fuelTypeService.GetFuelTypeByIdAsync(id);
            return Ok(fuelType);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
    }

    [HttpPost]
    public async Task<ActionResult<FuelType>> Create([FromBody] FuelTypeCreateDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var fuelType = await _fuelTypeService.AddFuelTypeAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = fuelType.Id }, fuelType);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<FuelType>> Update(Guid id, [FromBody] FuelTypeCreateDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        try
        {
            var updated = await _fuelTypeService.UpdateFuelTypeAsync(id, dto);
            return Ok(updated);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        try
        {
            await _fuelTypeService.DeleteFuelTypeAsync(id);
            return NoContent();
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
    }

    [HttpGet("by-station/{stationId}")]
    public async Task<ActionResult<IEnumerable<FuelType>>> GetByStationId(Guid stationId)
    {
        var result = await _fuelTypeService.GetFuelTypesByStationIdAsync(stationId);
        return Ok(result);
    }
}
