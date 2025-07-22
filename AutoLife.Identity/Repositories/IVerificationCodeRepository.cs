using AutoLife.Identity.Models.IdentityEntities;
using AutoLife.Persistence.Repositories;

namespace AutoLife.Identity.Repositories;

public interface IVerificationCodeRepository : IGenericRepository<VerificationCode>
{
    Task<VerificationCode?> GetCodeAsync(string email);
    Task<bool> VerifyCodeAsync(string phoneNumber, string code);
    Task<bool> ExistsAsync(string phoneNumber);
    Task AddCodeAsync(string phoneNumber, string code);
    Task DeleteCodeAsync(string phoneNumber);
}