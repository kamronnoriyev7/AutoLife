using AutoLife.Api.Controllers.BaseController;
using AutoLife.Api.Extensions;
using AutoLife.Identity.Models.AuthDTOs.Requests;
using AutoLife.Identity.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace AutoLife.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService userService)
    {
        _authService = userService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterRequestDto dto)
    {
        await _authService.RegisterAsync(dto);
        return Ok("Registered");
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequestDto dto)
    {
        var result = await _authService.LoginAsync(dto);
        return Ok(result); // result -> AccessToken, RefreshToken
    }


    [HttpPost("refresh-token")]
    public async Task<IActionResult> Refresh([FromBody] RefreshTokenRequestDto dto)
    {
        var result = await _authService.RefreshTokenAsync(dto);
        return Ok(result);
    }

    [HttpPost("change-password")]
    [Authorize]
    public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordRequestDto request)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        // JWT token ichidagi foydalanuvchi ID sini olish
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
        if (userIdClaim == null)
            return Unauthorized("Foydalanuvchi aniqlanmadi.");

        Guid userId = Guid.Parse(userIdClaim.Value);

        await _authService.ChangePasswordAsync(userId, request);
        return Ok(new { message = "Parol muvaffaqiyatli o‘zgartirildi." });
    }

    [Authorize]
    [HttpPost("logout")]
    public async Task<IActionResult> Logout(string refreshToken)
    {
        await _authService.LogoutAsync(refreshToken);

        return Ok(new { message = "Logged out successfully" });
    }

}
