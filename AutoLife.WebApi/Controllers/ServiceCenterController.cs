using AutoLife.Application.DTOs.ServiceCentersDTOs;
using AutoLife.Infrastructure.Services.ServiceCenterServices;
using Microsoft.AspNetCore.Mvc;

namespace AutoLife.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ServiceCenterController : ControllerBase
{
    private readonly IServiceCenterService _serviceCenterService;

    public ServiceCenterController(IServiceCenterService serviceCenterService)
    {
        _serviceCenterService = serviceCenterService;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateServiceCenterDto dto)
    {
        await _serviceCenterService.AddServiceCenterAsync(dto);
        return Ok("Service center created successfully.");
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await _serviceCenterService.GetAllServiceCentersAsync();
        return Ok(result);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var result = await _serviceCenterService.GetServiceCenterByIdAsync(id);
        return Ok(result);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] CreateServiceCenterDto dto)
    {
        await _serviceCenterService.UpdateServiceCenterAsync(id, dto);
        return Ok("Service center updated successfully.");
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _serviceCenterService.DeleteServiceCenterAsync(id);
        return Ok("Service center deleted (soft) successfully.");
    }

    [HttpGet("by-location")]
    public async Task<IActionResult> GetByLocation([FromQuery] string location)
    {
        var result = await _serviceCenterService.GetServiceCentersByLocationAsync(location);
        return Ok(result);
    }

    [HttpGet("by-type")]
    public async Task<IActionResult> GetByType([FromQuery] string type)
    {
        var result = await _serviceCenterService.GetServiceCentersByTypeAsync(type);
        return Ok(result);
    }
}
