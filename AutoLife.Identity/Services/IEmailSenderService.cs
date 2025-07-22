using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoLife.Identity.Services;

public interface IEmailSenderService
{
    Task SendAsync(string to, string subject, string body);
}
