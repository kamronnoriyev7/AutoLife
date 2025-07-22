using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoLife.Identity.Models.IdentityEntities;

public class SmtpSettings
{
    public string Host { get; set; } = default!;        // SMTP server manzili (masalan: smtp.gmail.com)
    public int Port { get; set; }                       // SMTP porti (masalan: 587)
    public string Username { get; set; } = default!;    // Email (yuboruvchi)
    public string Password { get; set; } = default!;    // Email paroli yoki app password
    public string From { get; set; } = default!;        // Kimdan yuborilayotganini ko'rsatish uchun
    public bool EnableSsl { get; set; }                 // SSL kerakmi yoki yo‘qmi
}
