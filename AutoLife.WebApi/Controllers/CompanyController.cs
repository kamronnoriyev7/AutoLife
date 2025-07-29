using AutoLife.Application.DTOs.CompanyDTOs;
using AutoLife.Infrastructure.Services.CompanyServices;
using Microsoft.AspNetCore.Mvc;

namespace AutoLife.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CompanyController : ControllerBase
{
    private readonly ICompanyService _companyService;

    public CompanyController(ICompanyService companyService)
    {
        _companyService = companyService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var companies = await _companyService.GetAllAsync();
        return Ok(companies);
    }

    [HttpGet("details")]
    public async Task<IActionResult> GetAllWithDetails(CancellationToken cancellationToken)
    {
        var companies = await _companyService.GetAllWithDetailsAsync(cancellationToken);
        return Ok(companies);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var company = await _companyService.GetByIdAsync(id);
        if (company == null)
            return NotFound($"Company with ID {id} not found.");
        return Ok(company);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CompanyCreateDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var created = await _companyService.CreateAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] CompanyUpdateDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var updated = await _companyService.UpdateAsync(id, dto);
        if (updated == null)
            return NotFound($"Company with ID {id} not found.");

        return Ok(updated);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var result = await _companyService.DeleteAsync(id);
        if (!result)
            return NotFound($"Company with ID {id} not found or already deleted.");
        return NoContent();
    }
}
