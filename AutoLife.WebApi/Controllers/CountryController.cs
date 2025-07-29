using AutoLife.Application.DTOs.CountryDTOs;
using AutoLife.Infrastructure.Services.CountryServices;
using Microsoft.AspNetCore.Mvc;

namespace AutoLife.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CountryController : ControllerBase
{
    private readonly ICountryService _countryService;

    public CountryController(ICountryService countryService)
    {
        _countryService = countryService;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateCountryDto dto)
    {
        var result = await _countryService.CreateAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await _countryService.GetAllAsync();
        return Ok(result);
    }

    [HttpGet("details")]
    public async Task<IActionResult> GetAllWithDetails(CancellationToken cancellationToken)
    {
        var result = await _countryService.GetAllWithDetailsAsync(cancellationToken);
        return Ok(result);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var result = await _countryService.GetByIdAsync(id);
        if (result is null) return NotFound("Country not found.");
        return Ok(result);
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdateCountryDto dto)
    {
        var result = await _countryService.UpdateAsync(dto);
        return Ok(result);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var success = await _countryService.DeleteAsync(id);
        if (!success) return NotFound("Country not found.");
        return NoContent();
    }
}
