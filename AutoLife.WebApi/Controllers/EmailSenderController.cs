using AutoLife.Application.DTOs;
using AutoLife.Identity.Models.AuthDTOs.Requests;
using AutoLife.Identity.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace AutoLife.Identity.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EmailSenderController : ControllerBase
{
    private readonly IEmailSenderService _emailSenderService;

    public EmailSenderController(IEmailSenderService emailSenderService)
    {
        _emailSenderService = emailSenderService;
    }

    [HttpPost("send")]
    public async Task<IActionResult> SendEmail([FromBody] EmailDto dto)
    {
        await _emailSenderService.SendAsync(dto.To, dto.Subject, dto.Body);
        return Ok("Email yuborildi");
    }

    [HttpPost("send-email-with-attachment")]
    public async Task<IActionResult> SendEmailWithAttachment([FromForm] EmailFileHelper response)
    {
        if (response.Attachment == null ||response.Attachment.Length == 0)
            return BadRequest("Fayl yuborilmadi.");

        using var ms = new MemoryStream();
        await response.Attachment.CopyToAsync(ms);
        var fileBytes = ms.ToArray();
        var fileName = response.Attachment.FileName;

        await _emailSenderService.SendAsync(response.To!, response.Subject!, response.Body!, fileBytes, fileName);
        return Ok("Email fayl bilan yuborildi.");
    }
}


