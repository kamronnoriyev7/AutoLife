using AutoLife.Infrastructure.Mappers;
using AutoLife.Infrastructure.Services.AuthServices;
using AutoLife.Infrastructure.Services.TokenServices;
using AutoLife.Persistence.DataBaseContext;
using AutoLife.Persistence.Repositories;
using AutoLife.Persistence.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoLife.Infrastructure.Extensions;

public static class DependencyInjection
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration config)
    {
        // Connection stringni appsettings.json dan olish
        var connectionString = config.GetConnectionString("DefaultConnection");

        services.AddDbContext<AppDbContext>(options =>
        {
            options.UseNpgsql(connectionString);
        });

        // Generic Repository va UnitOfWork
        services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        // Auth va boshqa servislar
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<ITokenService, TokenService>();

        // Mapping profillarni ro‘yxatdan o‘tkazish
        services.RegisterMappingProfiles(); // bu sening static methoding

        // Mapping wrapper (agar ishlatsang)
        services.AddScoped<IMappingService, MappingService>();

        return services;
    }
}
