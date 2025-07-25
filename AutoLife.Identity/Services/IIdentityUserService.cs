using AutoLife.Identity.Models.AuthDTOs.Requests;
using AutoLife.Identity.Models.AuthDTOs.Responses;

namespace AutoLife.Identity.Services;

public interface IIdentityUserService
{
    Task<IEnumerable<UserInfoResponse>> GetAllAsync();
    Task<UserInfoResponse?> GetByIdAsync(Guid id);
    Task<Guid> CreateAsync(RegisterRequestDto dto, Guid password);
    Task<bool> DeleteAsync(Guid id);
    Task<bool> UpdateAsync(Guid id, UserInfoResponse dto);
    Task<bool> IsEmailExistsAsync(string email);
    Task<bool> IsUserNameExistsAsync(string userName);
    Task Remove(Guid id);

}