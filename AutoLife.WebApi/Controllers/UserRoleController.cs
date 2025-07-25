using AutoLife.Identity.Models.AuthDTOs.Requests;
using AutoLife.Identity.Models.IdentityEntities;
using AutoLife.Identity.Services;
using Microsoft.AspNetCore.Mvc;

namespace AutoLife.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserRoleController : ControllerBase
{
    private readonly IUserRoleService _userRoleService;

    public UserRoleController(IUserRoleService userRoleService)
    {
        _userRoleService = userRoleService;
    }

    // GET: api/UserRole
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var roles = await _userRoleService.GetAllRolesAsync();
        return Ok(roles);
    }

    // GET: api/UserRole/{id}
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var role = await _userRoleService.GetByIdAsync(id);
        if (role == null) return NotFound();
        return Ok(role);
    }

    // POST: api/UserRole
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateUserRoleDto dto)
    {
        var role = await _userRoleService.CreateAsync(dto.Name, dto.Description);
        return CreatedAtAction(nameof(GetById), new { id = role.Id }, role);
    }

    // PUT: api/UserRole/{id}
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateUserRoleDto dto)
    {
        var success = await _userRoleService.UpdateAsync(id, dto.Name, dto.Description);
        if (!success) return NotFound();
        return NoContent();
    }

    // DELETE: api/UserRole/{id}
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var success = await _userRoleService.DeleteAsync(id);
        if (!success) return NotFound();
        return NoContent();
    }

    // GET: api/UserRole/user/{userId}
    [HttpGet("user/{userId}")]
    public async Task<IActionResult> GetAllByUserId(Guid userId)
    {
        var roles = await _userRoleService.GetAllByUserIdAsync(userId);
        return Ok(roles);
    }
}
