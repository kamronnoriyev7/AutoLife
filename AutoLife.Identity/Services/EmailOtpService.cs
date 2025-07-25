using AutoLife.Identity.Models.IdentityEntities;
using AutoLife.Identity.Repositories;
using AutoLife.Persistence.UnitOfWork;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoLife.Identity.Services;

public class EmailOtpService : IEmailOtpService
{
    private readonly IEmailSenderService _emailSender;
    private readonly IMemoryCache _cache;
    private readonly IdentityDbContext _context; // Assuming you have a DbContext for Identity
    private readonly IVerificationCodeRepository verificationCodeRepository;
    private readonly IUnitOfWork<IdentityDbContext> _unitOfWork;

    public EmailOtpService(IEmailSenderService emailSender, IMemoryCache cache, IdentityDbContext context, IVerificationCodeRepository verificationCodeRepository, IUnitOfWork<IdentityDbContext> unitOfWork)
    {
        _emailSender = emailSender;
        _cache = cache;
        _context = context;
        this.verificationCodeRepository = verificationCodeRepository;
        _unitOfWork = unitOfWork;
    }

    public bool IsEmailOtpSentRecently(string email)
    {
        // Check if the OTP was sent recently using cache
        return _cache.TryGetValue($"otp-sent:{email}", out _);
    }

    public async Task<bool> IsEmailVerifiedAsync(string email)
    {
        var code = await verificationCodeRepository.GetCodeAsync(email)
            ?? throw new Exception("Verification code not found for this email.");

        return  code.IsVerified;
    }

    public async Task ResendOtpAsync(string email)
    {
       await SendOtpAsync(email);
    }

    public async Task SendOtpAsync(string email)
    {
        if (_cache.TryGetValue($"otp-sent:{email}", out _))
            throw new Exception("Kod allaqachon yuborilgan. Iltimos, biroz kuting.");

        var otp = new Random().Next(100000, 999999).ToString();
        _cache.Set($"otp:{email}", otp, TimeSpan.FromMinutes(5));
        _cache.Set($"otp-sent:{email}", true, TimeSpan.FromSeconds(60)); // 1 daqiqa blok

        await _emailSender.SendAsync(email, "Tasdiqlash kodi", $"Sizning OTP kodingiz: {otp}");
    }

    public async Task<bool> VerifyEmailAsync(string email, string otp)
    {
        if (_cache.TryGetValue($"otp:{email}", out string? cachedOtp))
        {
            bool success = cachedOtp == otp;

            if (success)
            {
                _cache.Remove($"otp:{email}");

                // VerificationCode obyekti yaratish
                var code = new VerificationCode
                {
                    Id = Guid.NewGuid(),
                    Email = email,
                    Code = otp,
                    ExpireAt = DateTime.UtcNow.AddMinutes(2),
                    IsUsed = false,
                    IsVerified = true, // Email tasdiqlanganini belgilash
                    CreateDate = DateTime.UtcNow // BaseEntity dan meros
                };

                // Repository orqali saqlash
                await verificationCodeRepository.AddCodeAsync(code);
                await _unitOfWork.SaveChangesAsync(); // saqlash
            }

            return success;
        }

        return false;
    }

}

