using AutoLife.Identity.Models.IdentityEntities;
using AutoLife.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoLife.Identity.Repositories;

public class VerificationCodeRepository : GenericRepository<VerificationCode>, IVerificationCodeRepository
{
    public VerificationCodeRepository(IdentityDbContext context) : base(context) { }

    public async Task AddCodeAsync(string email, string code)
    {
        var existingCode = await _dbSet.FirstOrDefaultAsync(VerificationCode => VerificationCode.Email == email);
        if (existingCode != null)
        {
            existingCode.Code = code;
            existingCode.ExpireAt = DateTime.UtcNow.AddMinutes(5); // 5 daqiqa amal qilish muddati
            existingCode.IsUsed = true; 
            _dbSet.Update(existingCode);
        }
        else
        {
            var newCode = new VerificationCode
            {
                Email = email,
                Code = code,
                IsUsed = true, // Yangi kod uchun avtomatik ravishda ishlatilgan deb belgilash
                ExpireAt = DateTime.UtcNow.AddMinutes(5) // 5 daqiqa amal qilish muddati
            };
            await _dbSet.AddAsync(newCode);
        }
        await _context.SaveChangesAsync();
    }

    public async Task DeleteCodeAsync(string email)
    {
        var code = await _dbSet.FirstOrDefaultAsync(VerificationCode => VerificationCode.Email == email);
        if (code != null)
        {
            _dbSet.Remove(code);
            await _context.SaveChangesAsync();
        }
        else
        {
            throw new KeyNotFoundException($"Verification code for phone number {email} not found.");
        }
    }

    public async Task<bool> ExistsAsync(string phoneNumber)
    {
        return await _dbSet.AnyAsync(VerificationCode => VerificationCode.Email == phoneNumber);
    }

    public async Task<VerificationCode?> GetCodeAsync(string email)
    {
        return await _dbSet.FirstOrDefaultAsync(VerificationCode => VerificationCode.Email == email);
    }

    public async Task<bool> VerifyCodeAsync(string email, string code)
    {
        var verificationCode = await _dbSet.FirstOrDefaultAsync(vc => vc.Email == email && vc.Code == code);
        if (verificationCode != null && verificationCode.ExpireAt > DateTime.UtcNow)
        {
            // Code is valid and not expired
            return true;
        }
        return false; // Code is invalid or expired
    }
}
