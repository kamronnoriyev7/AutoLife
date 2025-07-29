using AutoLife.Application.DTOs.AddressDTOs;
using AutoLife.Infrastructure.Services.AddressServices;
using Microsoft.AspNetCore.Mvc;

namespace AutoLife.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AddressController : ControllerBase
{
    private readonly IAddressService _addressService;

    public AddressController(IAddressService addressService)
    {
        _addressService = addressService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await _addressService.GetAllAddressesAsync();
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var result = await _addressService.GetAddressByIdAsync(id);
        if (result == null)
            return NotFound();

        return Ok(result);
    }

    [HttpGet("user/{userId}")]
    public async Task<IActionResult> GetByUserId(Guid userId)
    {
        var result = await _addressService.GetAddressesByUserIdAsync(userId);
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateAddressDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var created = await _addressService.CreateAddressAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateAddressDto dto)
    {
        if (id != dto.Id)
            return BadRequest("ID mismatch");

        var updated = await _addressService.UpdateAddressAsync(dto);
        return Ok(updated);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var success = await _addressService.DeleteAddressAsync(id);
        if (!success)
            return NotFound();

        return NoContent();
    }
}
