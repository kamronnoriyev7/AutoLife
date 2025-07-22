using AutoLife.Identity.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace AutoLife.Identity.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EmailOtpController : ControllerBase
{
    private readonly IEmailOtpService _emailOtpService;

    public EmailOtpController(IEmailOtpService emailOtpService)
    {
        _emailOtpService = emailOtpService;
    }

    [HttpPost("send")]
    public async Task<IActionResult> SendOtp([FromBody] string email)
    {
        await _emailOtpService.SendOtpAsync(email);
        return Ok("OTP yuborildi");
    }

    [HttpPost("verify")]
    public async Task<IActionResult> VerifyOtp([FromQuery] string email, [FromQuery] string otp)
    {
        var result = await _emailOtpService.VerifyEmailAsync(email, otp);
        return result ? Ok("OTP to‘g‘ri") : BadRequest("Noto‘g‘ri OTP");
    }

    [HttpPost("verify-email")]
    public async Task<IActionResult> VerifyEmail([FromQuery] string email, [FromQuery] string otp)
    {
        var result = await _emailOtpService.VerifyEmailAsync(email, otp);
        return result ? Ok("Email tasdiqlandi") : BadRequest("Xato kod");
    }

    [HttpGet("is-verified")]
    public async Task<IActionResult> IsEmailVerified([FromQuery] string email)
    {
        var result = await _emailOtpService.IsEmailVerifiedAsync(email);
        return Ok(result);
    }

    [HttpPost("resend")]
    public async Task<IActionResult> ResendOtp([FromBody] string email)
    {
        await _emailOtpService.ResendOtpAsync(email);
        return Ok("OTP qayta yuborildi");
    }
}
