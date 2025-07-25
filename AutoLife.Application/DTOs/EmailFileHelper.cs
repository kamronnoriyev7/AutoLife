using Microsoft.AspNetCore.Http;

namespace AutoLife.Application.DTOs;

public class EmailFileHelper
{
    public string? To { get; set; }
    public string? Subject { get; set; }
    public string? Body { get; set; }
    public IFormFile? Attachment { get; set; }
}
