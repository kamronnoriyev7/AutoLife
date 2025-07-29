using AutoLife.Application.DTOs.UsersDTOs;
using AutoLife.Domain.Entities;
using AutoLife.Infrastructure.Services.UserServices;
using Microsoft.AspNetCore.Mvc;

namespace AutoLife.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpPost]
    [Consumes("multipart/form-data")]
    public async Task<IActionResult> Create([FromForm] UserCreateDto dto)
    {
        var result = await _userService.CreateUserAsync(dto);
        return CreatedAtAction(nameof(Create), new { id = result.Id }, result);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetUserById(Guid id)
    {
        var user = await _userService.GetUserByIdAsync(id);
        return Ok(user);
    }

    [HttpGet("email/{email}")]
    public async Task<IActionResult> GetUserByEmail(string email)
    {
        var user = await _userService.GetUserByEmailAsync(email);
        return Ok(user);
    }

    [HttpGet("phone/{phoneNumber}")]
    public async Task<IActionResult> GetUserByPhoneNumber(string phoneNumber)
    {
        var user = await _userService.GetUserByPhoneNumberAsync(phoneNumber);
        return Ok(user);
    }

    [HttpGet("username/{userName}")]
    public async Task<IActionResult> GetUserByUserName(string userName)
    {
        var user = await _userService.GetUserByUserNameAsync(userName);
        return Ok(user);
    }

    [HttpGet("all")]
    public async Task<IActionResult> GetAllUsers()
    {
        var users = await _userService.GetAllUsersAsync();
        return Ok(users);
    }

    [HttpPut("update")]
    public async Task<IActionResult> UpdateUser([FromBody] UserUpdateDto dto)
    {
        var result = await _userService.UpdateUserAsync(dto);

        return Ok(result);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteUser(Guid id)
    {
        var result = await _userService.DeleteUserAsync(id);
        return result ? Ok("User deleted successfully.") : NotFound("User not found.");
    }

    [HttpGet("exists/{id:guid}")]
    public async Task<IActionResult> CheckUserExists(Guid id)
    {
        var exists = await _userService.UserExistsAsync(id);
        return Ok(exists);
    }

    [HttpGet("exists-by-email/{email}")]
    public async Task<IActionResult> CheckUserExistsByEmail(string email)
    {
        var exists = await _userService.UserExistsByEmailAsync(email);
        return Ok(exists);
    }

    [HttpGet("exists-by-phone/{phoneNumber}")]
    public async Task<IActionResult> CheckUserExistsByPhone(string phoneNumber)
    {
        var exists = await _userService.UserExistsByPhoneNumberAsync(phoneNumber);
        return Ok(exists);
    }

    [HttpGet("exists-by-username/{userName}")]
    public async Task<IActionResult> CheckUserExistsByUserName(string userName)
    {
        var exists = await _userService.UserExistsByUserNameAsync(userName);
        return Ok(exists);
    }
}
