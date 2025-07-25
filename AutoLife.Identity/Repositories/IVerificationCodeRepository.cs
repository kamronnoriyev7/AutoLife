using AutoLife.Identity.Models.IdentityEntities;
using AutoLife.Persistence.Repositories;

namespace AutoLife.Identity.Repositories;

public interface IVerificationCodeRepository : IGenericRepository<VerificationCode>
{
    Task<VerificationCode?> GetCodeAsync(string email);
    Task<bool> VerifyCodeAsync(string email, string code);
    Task<bool> ExistsAsync(VerificationCode verificationCode);
    Task AddCodeAsync(VerificationCode verificationCode);
    Task DeleteCodeAsync(string email);
}