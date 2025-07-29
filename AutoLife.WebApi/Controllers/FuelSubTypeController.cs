using AutoLife.Application.DTOs.FuelSubTypeDTOs;
using AutoLife.Domain.Entities;
using AutoLife.Infrastructure.Services.FuelSubTypeServices;
using Microsoft.AspNetCore.Mvc;

namespace AutoLife.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class FuelSubTypeController : ControllerBase
{
    private readonly IFuelSubTypeService _fuelSubTypeService;

    public FuelSubTypeController(IFuelSubTypeService fuelSubTypeService)
    {
        _fuelSubTypeService = fuelSubTypeService;
    }

    // POST: api/FuelSubType
    [HttpPost]
    public async Task<ActionResult<FuelSubType>> Create([FromBody] FuelSubTypeCreateDto dto, CancellationToken cancellationToken)
    {
        var result = await _fuelSubTypeService.CreateAsync(dto, cancellationToken);
        return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
    }

    // PUT: api/FuelSubType/{id}
    [HttpPut("{id:guid}")]
    public async Task<ActionResult<FuelSubType>> Update(Guid id, [FromBody] FuelSubTypeCreateDto dto, CancellationToken cancellationToken)
    {
        var result = await _fuelSubTypeService.UpdateAsync(id, dto, cancellationToken);
        return Ok(result);
    }

    // DELETE: api/FuelSubType/{id}
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
    {
        await _fuelSubTypeService.DeleteAsync(id, cancellationToken);
        return NoContent();
    }

    // GET: api/FuelSubType
    [HttpGet]
    public async Task<ActionResult<IEnumerable<FuelSubType>>> GetAll(CancellationToken cancellationToken)
    {
        var result = await _fuelSubTypeService.GetAllAsync(cancellationToken);
        return Ok(result);
    }

    // GET: api/FuelSubType/{id}
    [HttpGet("{id:guid}")]
    public async Task<ActionResult<FuelSubType>> GetById(Guid id, CancellationToken cancellationToken)
    {
        var result = await _fuelSubTypeService.GetByIdAsync(id, cancellationToken);
        return Ok(result);
    }
}
