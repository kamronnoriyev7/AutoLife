using AutoLife.Identity.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace AutoLife.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class VerificationController : ControllerBase
{
    private readonly IEmailOtpService _emailOtpService;

    public VerificationController(IEmailOtpService emailOtpService)
    {
        _emailOtpService = emailOtpService;
    }

    [HttpPost("send-otp")]
    public async Task<IActionResult> SendOtp([FromQuery] string email)
    {
        try
        {
            await _emailOtpService.SendOtpAsync(email);
            return Ok("Tasdiqlash kodi email manzilingizga yuborildi.");
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpPost("verify-otp")]
    public async Task<IActionResult> VerifyOtp([FromQuery] string email, [FromQuery] string otp)
    {
        var result = await _emailOtpService.VerifyEmailAsync(email, otp);
        if (result)
            return Ok("Tasdiqlash kodi to‘g‘ri.");
        return BadRequest("Tasdiqlash kodi noto‘g‘ri yoki muddati tugagan.");
    }

    [HttpPost("verify-email")]
    public async Task<IActionResult> VerifyEmail([FromQuery] string email, [FromQuery] string otp)
    {
        var result = await _emailOtpService.VerifyEmailAsync(email, otp);
        if (result)
            return Ok("Email tasdiqlandi.");
        return BadRequest("Tasdiqlash kodi noto‘g‘ri yoki allaqachon ishlatilgan.");
    }

    [HttpGet("is-verified")]
    public async Task<IActionResult> IsEmailVerified([FromQuery] string email)
    {
        var isVerified = await _emailOtpService.IsEmailVerifiedAsync(email);
        return Ok(new { email, isVerified });
    }

    [HttpPost("resend-otp")]
    public async Task<IActionResult> ResendOtp([FromQuery] string email)
    {
        try
        {
            await _emailOtpService.ResendOtpAsync(email);
            return Ok("Kod qayta yuborildi.");
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }
}
