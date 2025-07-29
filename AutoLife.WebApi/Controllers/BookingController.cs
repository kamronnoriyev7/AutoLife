using AutoLife.Application.DTOs.BookingDTOs;
using AutoLife.Infrastructure.Services.BookingServices;
using Microsoft.AspNetCore.Mvc;

namespace AutoLife.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BookingController : ControllerBase
{
    private readonly IBookingService _bookingService;

    public BookingController(IBookingService bookingService)
    {
        _bookingService = bookingService;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] BookingCreateDto dto)
    {
        var result = await _bookingService.CreateAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await _bookingService.GetAllAsync();
        return Ok(result);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var result = await _bookingService.GetByIdAsync(id);
        if (result == null)
            return NotFound("Booking not found!");
        return Ok(result);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var success = await _bookingService.DeleteAsync(id);
        if (!success)
            return NotFound("Booking not found or already deleted!");
        return NoContent();
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] BookingUpdateDto dto)
    {
        var updated = await _bookingService.UpdateAsync(id, dto);
        if (updated == null)
            return NotFound("Booking not found for update!");
        return Ok(updated);
    }

    [HttpGet("user/{userId:guid}")]
    public async Task<IActionResult> GetAllByUserId(Guid userId, CancellationToken cancellationToken)
    {
        if (userId == Guid.Empty)
            return BadRequest("Invalid user ID");

        var bookings = await _bookingService.GetAllByUserIdAsync(userId, cancellationToken);
        return Ok(bookings);
    }

    [HttpGet("details")]
    public async Task<IActionResult> GetAllWithDetails(CancellationToken cancellationToken)
    {
        var result = await _bookingService.GetAllWithDetailsAsync(cancellationToken);
        return Ok(result);
    }
}
