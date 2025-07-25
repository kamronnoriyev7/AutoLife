using AutoLife.Identity.Models.AuthDTOs.Requests;
using AutoLife.Identity.Models.AuthDTOs.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoLife.Identity.Services;

public interface IAuthService
{
        /// <summary>
        /// Foydalanuvchini ro‘yxatdan o‘tkazadi
        /// </summary>
        Task<AuthResponse> RegisterAsync(RegisterRequestDto registerDto);

        /// <summary>
        /// Foydalanuvchini tizimga kiritadi
        /// </summary>
        Task<AuthResponse> LoginAsync(LoginRequestDto loginDto);

        /// <summary>
        /// Refresh token orqali yangi access va refresh token qaytaradi
        /// </summary>
        Task<AuthResponse> RefreshTokenAsync(RefreshTokenRequestDto refreshTokenDto);

        /// <summary>
        /// Foydalanuvchi parolini o‘zgartiradi
        /// </summary>
        Task ChangePasswordAsync(Guid identityUserId, ChangePasswordRequestDto changePasswordDto);
        /// <summary>
        /// Foydalanuvchini tizimdan chiqadi
        /// </summary>
        Task LogoutAsync(string refreshToken);
}
