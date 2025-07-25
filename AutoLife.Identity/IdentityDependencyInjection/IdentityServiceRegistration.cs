using AutoLife.Identity.Models.IdentityEntities;
using AutoLife.Identity.Repositories;
using AutoLife.Identity.Services;
using AutoLife.Identity.Validators;
using AutoLife.Persistence.UnitOfWork;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using IdentityUser = AutoLife.Identity.Models.IdentityEntities.IdentityUser;


namespace AutoLife.Identity.IdentityDependencyInjection;

public static class IdentityServiceRegistration
{
    public static IServiceCollection AddIdentityServices(this IServiceCollection services, IConfiguration config)
    {

        services.AddDbContext<IdentityDbContext>(options =>
           options.UseSqlServer(config.GetConnectionString("MSSQLConnection")));

        // appsettings.json dan o‘qish
        services.Configure<JwtSettings>(config.GetSection("Jwt"));

        // Serviceni ro‘yxatdan o‘tkazish

        services.AddMemoryCache();
        services.AddScoped<IEmailOtpService, EmailOtpService>();

        services.AddValidatorsFromAssemblyContaining<RegisterRequestDtoValidator>();
        services.AddValidatorsFromAssemblyContaining<ChangePasswordRequestDtoValidator>();
        services.AddValidatorsFromAssemblyContaining<RefreshTokenRequestDtoValidator>();
        services.AddValidatorsFromAssemblyContaining<LoginRequestDtoValidator>();

        services.AddScoped<IUnitOfWork<IdentityDbContext>, UnitOfWork<IdentityDbContext>>();

        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<ITokenService, TokenService>();
        services.AddScoped<IIdentityUserRepository, IdentityUserRepository>();
        services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();
        services.AddScoped<IRefreshTokenService, RefreshTokenService>();
        services.AddScoped<IPasswordHasherService, PasswordHasherService>();
        services.AddScoped<IUserRoleService, UserRoleService>();
        services.AddScoped<IIdentityUserRoleRepository, IdentityUserRoleRepository>();
        services.AddScoped<IVerificationCodeRepository , VerificationCodeRepository>();

        services.Configure<SmtpSettings>(config.GetSection("Email:Smtp"));
        services.AddTransient<IEmailSenderService, EmailSenderService>();
        services.AddScoped<IEmailOtpService, EmailOtpService>();
        services.AddScoped<IVerificationCodeRepository, VerificationCodeRepository>();



        return services;
    }
}

