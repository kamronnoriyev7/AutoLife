using AutoLife.Application.DTOs.AppFeedbackDTOs;
using AutoLife.Domain.Enums;
using AutoLife.Infrastructure.Services.AppFeedbackServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AutoLife.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AppFeedbackController : ControllerBase
{
    private readonly IAppFeedbackService _feedbackService;

    public AppFeedbackController(IAppFeedbackService feedbackService)
    {
        _feedbackService = feedbackService;
    }

    [HttpPost]
    [ProducesResponseType(typeof(AppFeedbackResponseDto), StatusCodes.Status201Created)]
    public async Task<IActionResult> Create([FromBody] AppFeedbackCreateDto dto)
    {
        var result = await _feedbackService.CreateAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<AppFeedbackResponseDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll([FromQuery] bool includeDeleted = false)
    {
        var result = await _feedbackService.GetAllAsync(includeDeleted);
        return Ok(result);
    }

    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(AppFeedbackResponseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(Guid id)
    {
        var result = await _feedbackService.GetByIdAsync(id);
        return Ok(result);
    }

    [HttpGet("by-user/{userId:guid}")]
    [ProducesResponseType(typeof(IEnumerable<AppFeedbackResponseDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetByUserId(Guid userId)
    {
        var result = await _feedbackService.GetByUserIdAsync(userId);
        return Ok(result);
    }

    [HttpGet("by-type/{type}")]
    [ProducesResponseType(typeof(IEnumerable<AppFeedbackResponseDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetByType(FeedbackType type)
    {
        var result = await _feedbackService.GetByTypeAsync(type);
        return Ok(result);
    }

    [HttpPut("{id:guid}")]
    [ProducesResponseType(typeof(AppFeedbackResponseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(Guid id, [FromBody] AppFeedbackUpdateDto dto)
    {
        var result = await _feedbackService.UpdateAsync(id, dto);
        return Ok(result);
    }

    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _feedbackService.DeleteAsync(id);
        return NoContent();
    }

    [HttpPatch("soft-delete/{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> SoftDelete(Guid id)
    {
        var result = await _feedbackService.SoftDeleteAsync(id);
        return Ok(new { success = result });
    }

    [HttpGet("count")]
    [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
    public async Task<IActionResult> Count()
    {
        var count = await _feedbackService.CountAsync();
        return Ok(count);
    }
}
