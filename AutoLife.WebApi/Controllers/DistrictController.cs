using AutoLife.Application.DTOs.DistrictDTOs;
using AutoLife.Infrastructure.Services.DistrictServices;
using Microsoft.AspNetCore.Mvc;

namespace AutoLife.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DistrictController : ControllerBase
{
    private readonly IDistrictService _districtService;

    public DistrictController(IDistrictService districtService)
    {
        _districtService = districtService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        var result = await _districtService.GetAllAsync(cancellationToken);
        return Ok(result);
    }

    [HttpGet("details")]
    public async Task<IActionResult> GetAllWithDetails(CancellationToken cancellationToken)
    {
        var result = await _districtService.GetAllWithDetailsAsync(cancellationToken);
        return Ok(result);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
    {
        var result = await _districtService.GetByIdAsync(id, cancellationToken);
        if (result is null) return NotFound($"District with ID {id} not found.");
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] DistrictCreateDto dto, CancellationToken cancellationToken)
    {
        var created = await _districtService.CreateAsync(dto, cancellationToken);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] DistrictCreateDto dto, CancellationToken cancellationToken)
    {
        var updated = await _districtService.UpdateAsync(id, dto, cancellationToken);
        return Ok(updated);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
    {
        var success = await _districtService.DeleteAsync(id, cancellationToken);
        return success ? Ok("Deleted successfully.") : BadRequest("Could not delete.");
    }
}
