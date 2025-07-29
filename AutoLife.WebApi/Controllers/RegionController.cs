using AutoLife.Application.DTOs.RegionDTOs;
using AutoLife.Infrastructure.Services.RegionServices;
using Microsoft.AspNetCore.Mvc;

namespace AutoLife.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RegionController : ControllerBase
{
    private readonly IRegionService _regionService;

    public RegionController(IRegionService regionService)
    {
        _regionService = regionService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateRegion([FromBody] CreateRegionDto regionDto)
    {
        var result = await _regionService.AddRegionAsync(regionDto);
        return CreatedAtAction(nameof(GetRegionById), new { id = result.Id }, result);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllRegions()
    {
        var result = await _regionService.GetAllRegionsAsync();
        return Ok(result);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetRegionById(Guid id)
    {
        var result = await _regionService.GetRegionByIdAsync(id);
        return Ok(result);
    }

    [HttpGet("by-country/{countryId:guid}")]
    public async Task<IActionResult> GetRegionsByCountryId(Guid countryId)
    {
        var result = await _regionService.GetRegionsByCountryIdAsync(countryId);
        return Ok(result);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdateRegion(Guid id, [FromBody] CreateRegionDto regionDto)
    {
        var result = await _regionService.UpdateRegionAsync(id, regionDto);
        return Ok(result);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteRegion(Guid id)
    {
        await _regionService.DeleteRegionAsync(id);
        return NoContent();
    }
}
