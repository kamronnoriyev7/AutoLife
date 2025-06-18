using AutoLife.Application.DTOs.AuthDTOs;
using AutoLife.Application.DTOs.UsersDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoLife.Infrastructure.Services.AuthServices;

public interface IAuthService
{
    Task<UserResponseDto> LoginAsync(LoginRequestDto loginDto);
    Task<UserResponseDto> SignUpAsync(RegisterRequestDto signUpDto);
    Task LogoutAsync(string username);
}

