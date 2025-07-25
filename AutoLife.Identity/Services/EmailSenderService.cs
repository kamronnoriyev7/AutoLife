using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace AutoLife.Identity.Services;

public class EmailSenderService : IEmailSenderService
{
    private readonly IConfiguration _config;

    public EmailSenderService(IConfiguration config)
    {
        _config = config;
    }

    public async Task SendAsync(string to, string subject, string body)
    {
        string? host = _config["Email:Smtp:Host"];
        string? portStr = _config["Email:Smtp:Port"];
        string? username = _config["Email:Smtp:Username"];
        string? password = _config["Email:Smtp:Password"];
        string? from = _config["Email:Smtp:From"];

        if (string.IsNullOrWhiteSpace(host) ||
            string.IsNullOrWhiteSpace(portStr) ||
            string.IsNullOrWhiteSpace(username) ||
            string.IsNullOrWhiteSpace(password) ||
            string.IsNullOrWhiteSpace(from))
        {
            throw new InvalidOperationException("SMTP konfiguratsiyasi to'liq emas!");
        }

        if (!int.TryParse(portStr, out int port))
        {
            throw new InvalidOperationException("SMTP port noto'g'ri formatda!");
        }

        using var smtp = new SmtpClient
        {
            Host = host,
            Port = port,
            EnableSsl = true,
            Credentials = new NetworkCredential(username, password)
        };

        using var message = new MailMessage(from, to, subject, body);
        await smtp.SendMailAsync(message);
    }

    public async Task SendAsync(string to, string subject, string body, byte[] attachmentBytes, string attachmentFileName)
    {
        string? host = _config["Email:Smtp:Host"];
        string? portStr = _config["Email:Smtp:Port"];
        string? username = _config["Email:Smtp:Username"];
        string? password = _config["Email:Smtp:Password"];
        string? from = _config["Email:Smtp:From"];

        if (string.IsNullOrWhiteSpace(host) ||
            string.IsNullOrWhiteSpace(portStr) ||
            string.IsNullOrWhiteSpace(username) ||
            string.IsNullOrWhiteSpace(password) ||
            string.IsNullOrWhiteSpace(from))
        {
            throw new InvalidOperationException("SMTP konfiguratsiyasi to'liq emas!");
        }

        if (!int.TryParse(portStr, out int port))
        {
            throw new InvalidOperationException("SMTP port noto'g'ri formatda!");
        }

        using var smtp = new SmtpClient
        {
            Host = host,
            Port = port,
            EnableSsl = true,
            Credentials = new NetworkCredential(username, password)
        };

        using var message = new MailMessage(from, to, subject, body);
        message.IsBodyHtml = true;

        // ✅ Fayl ilova qilish
        using var stream = new MemoryStream(attachmentBytes);
        var attachment = new Attachment(stream, attachmentFileName);
        message.Attachments.Add(attachment);

        await smtp.SendMailAsync(message);
    }

}
