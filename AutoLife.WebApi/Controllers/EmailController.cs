using AutoLife.Identity.Models.AuthDTOs.Requests;
using AutoLife.Identity.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace AutoLife.Identity.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EmailController : ControllerBase
{
    private readonly IEmailSenderService _emailSenderService;

    public EmailController(IEmailSenderService emailSenderService)
    {
        _emailSenderService = emailSenderService;
    }

    [HttpPost("send")]
    public async Task<IActionResult> SendEmail([FromBody] EmailDto dto)
    {
        await _emailSenderService.SendAsync(dto.To, dto.Subject, dto.Body);
        return Ok("Email yuborildi");
    }
}


