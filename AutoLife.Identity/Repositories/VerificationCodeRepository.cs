using AutoLife.Identity.Models.IdentityEntities;
using AutoLife.Persistence.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoLife.Identity.Repositories;

public class VerificationCodeRepository(IdentityDbContext context) : GenericRepository<VerificationCode, IdentityDbContext>(context), IVerificationCodeRepository
{
    public async Task AddCodeAsync(VerificationCode verificationCode)
    {
        var existingCode = await ExistsAsync(verificationCode);
        if (existingCode)
        {
            // Agar kod mavjud bo'lsa, uni yangilash
            var code = await _dbSet.FirstOrDefaultAsync(vc => vc.Email == verificationCode.Email);
            if (code != null)
            {
                code.Code = verificationCode.Code;
                code.ExpireAt = verificationCode.ExpireAt;
                code.IsUsed = verificationCode.IsUsed;
                code.CreateDate = verificationCode.CreateDate;
                _dbSet.Update(code);
            }
        }
        else
        {
            // Yangi kod qo'shish
            await _dbSet.AddAsync(verificationCode);
        }

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

    public async Task<bool> ExistsAsync(VerificationCode verificationCode)
    {
        return await _dbSet.AnyAsync(vc => vc.Email == verificationCode.Email);
    }

    public async Task<VerificationCode?> GetCodeAsync(string email)
    {
        return await _dbSet.FirstOrDefaultAsync(VerificationCode => VerificationCode.Email == email 
                                                                     && VerificationCode.IsDeleted == false);
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
