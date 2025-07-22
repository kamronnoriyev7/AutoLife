using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoLife.Identity.Services;

public interface IEmailOtpService
{
    Task SendOtpAsync(string email);
    Task<bool> VerifyEmailAsync(string email, string otp); // New method for email verification
    Task<bool> IsEmailVerifiedAsync(string email); // New method to check if email is verified
    Task ResendOtpAsync(string email); // New method to resend OTP if needed
}
