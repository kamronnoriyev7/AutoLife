using AutoLife.Identity.Models.AuthDTOs.Requests;
using AutoLife.Identity.Services;
using Microsoft.AspNetCore.Mvc;

namespace AutoLife.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class IdentityUserController : ControllerBase
{
    private readonly IIdentityUserService _identityUserService;

    public IdentityUserController(IIdentityUserService identityUserService)
    {
        _identityUserService = identityUserService;
    }

    /// <summary>
    /// Register new identity user
    /// </summary>
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterRequestDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var identityId = await _identityUserService.CreateAsync(dto, Guid.NewGuid());
        return CreatedAtAction(nameof(GetById), new { id = identityId }, new { identityId });
    }

    /// <summary>
    /// Get identity user by ID
    /// </summary>
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var user = await _identityUserService.GetByIdAsync(id);
        if (user == null)
            return NotFound($"User with ID {id} not found.");

        return Ok(user);
    }

    /// <summary>
    /// Get all identity users
    /// </summary>
    [HttpGet("all")]
    public async Task<IActionResult> GetAll()
    {
        var users = await _identityUserService.GetAllAsync();
        return Ok(users);
    }

    /// <summary>
    /// Update user by ID
    /// </summary>
    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] IdentityUserUpdateDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var result = await _identityUserService.UpdateAsync(id, dto);
        if (!result)
            return NotFound($"User with ID {id} not found or update failed.");

        return Ok("User updated successfully.");
    }

    /// <summary>
    /// Delete user (soft delete)
    /// </summary>
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var result = await _identityUserService.DeleteAsync(id);
        if (!result)
            return NotFound($"User with ID {id} not found.");

        return Ok("User deleted successfully.");
    }

    /// <summary>
    /// Check if email exists
    /// </summary>
    [HttpGet("exists/email")]
    public async Task<IActionResult> IsEmailExists([FromQuery] string email)
    {
        var exists = await _identityUserService.IsEmailExistsAsync(email);
        return Ok(exists);
    }

    /// <summary>
    /// Check if username exists
    /// </summary>
    [HttpGet("exists/username")]
    public async Task<IActionResult> IsUserNameExists([FromQuery] string userName)
    {
        var exists = await _identityUserService.IsUserNameExistsAsync(userName);
        return Ok(exists);
    }

    /// <summary>
    /// Get IdentityUserId by PhoneNumber
    /// </summary>
    [HttpGet("by-phone")]
    public async Task<IActionResult> GetByPhone([FromQuery] string phoneNumber)
    {
        var id = await _identityUserService.GetIdentityIdByPhoneNumberAsync(phoneNumber);
        return Ok(new { IdentityId = id });
    }

    /// <summary>
    /// Get IdentityUserId by Email
    /// </summary>
    [HttpGet("by-email")]
    public async Task<IActionResult> GetByEmail([FromQuery] string email)
    {
        var id = await _identityUserService.GetIdentityIdByEmailAsync(email);
        return Ok(new { IdentityId = id });
    }

    /// <summary>
    /// Get UserId by UserName
    /// </summary>
    [HttpGet("userid-by-username")]
    public async Task<IActionResult> GetUserIdByUserName([FromQuery] string userName)
    {
        var userId = await _identityUserService.GetUserIdByIdentityIdAsync(userName);
        return Ok(new { UserId = userId });
    }
}
