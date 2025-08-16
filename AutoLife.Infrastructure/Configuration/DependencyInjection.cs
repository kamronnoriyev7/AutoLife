using AutoLife.Identity;
using AutoLife.Infrastructure.Mappers;
using AutoLife.Infrastructure.PaymentServices;
using AutoLife.Infrastructure.PaymentServices.ClickServices;
using AutoLife.Infrastructure.PaymentServices.PaymeServices;
using AutoLife.Infrastructure.Services.AddressServices;
using AutoLife.Infrastructure.Services.AppFeedbackServices;
using AutoLife.Infrastructure.Services.BookingServices;
using AutoLife.Infrastructure.Services.CompanyServices;
using AutoLife.Infrastructure.Services.CountryServices;
using AutoLife.Infrastructure.Services.DistrictServices;
using AutoLife.Infrastructure.Services.FavoriteServices;
using AutoLife.Infrastructure.Services.FuelPriceServices;
using AutoLife.Infrastructure.Services.FuelStationServices;
using AutoLife.Infrastructure.Services.FuelSubTypeServices;
using AutoLife.Infrastructure.Services.FuelTypeServices;
using AutoLife.Infrastructure.Services.NewsServices;
using AutoLife.Infrastructure.Services.NotificationServices;
using AutoLife.Infrastructure.Services.ParkingServices;
using AutoLife.Infrastructure.Services.RegionServices;
using AutoLife.Infrastructure.Services.ServiceCenterServices;
using AutoLife.Infrastructure.Services.UserServices;
using AutoLife.Infrastructure.Services.VehicleServices;
using AutoLife.Persistence.DataBaseContext;
using AutoLife.Persistence.Repositories;
using AutoLife.Persistence.Repositories.AddressRepositories;
using AutoLife.Persistence.Repositories.AppFeedbackRepositories;
using AutoLife.Persistence.Repositories.BookingRepositories;
using AutoLife.Persistence.Repositories.CompanyRepositories;
using AutoLife.Persistence.Repositories.CountryRepositories;
using AutoLife.Persistence.Repositories.DistrictRepositories;
using AutoLife.Persistence.Repositories.FavoriteRepositories;
using AutoLife.Persistence.Repositories.FuelPriceRepositories;
using AutoLife.Persistence.Repositories.FuelStationRepositories;
using AutoLife.Persistence.Repositories.FuelSubTypeRepositories;
using AutoLife.Persistence.Repositories.FuelTypeRepositories;
using AutoLife.Persistence.Repositories.NewsRepositories;
using AutoLife.Persistence.Repositories.NotificationRepositories;
using AutoLife.Persistence.Repositories.ParkingRepositories;
using AutoLife.Persistence.Repositories.RegionRepositories;
using AutoLife.Persistence.Repositories.ServiceCenterRepositories;
using AutoLife.Persistence.Repositories.UserRepositories;
using AutoLife.Persistence.Repositories.VehicleRepositories;
using AutoLife.Persistence.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoLife.Infrastructure.Configuration;

public static class DependencyInjection
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration config)
    {
        // Connection stringni appsettings.json dan olish
        var connectionString = config.GetConnectionString("PostgresConnection");

        services.AddDbContext<AppDbContext>(options =>
        {
            options.UseNpgsql(config.GetConnectionString("PostgresConnection"));
        });



        // Generic Repository and Unit of Work
        services.AddScoped(typeof(IGenericRepository<,>), typeof(GenericRepository<,>));
        services.AddScoped(typeof(IUnitOfWork<>), typeof(UnitOfWork<>));


        // Payment Providers
        services.AddScoped<IPaymentProvider, ClickPaymentProvider>();
        services.AddScoped<IPaymentProvider, PaymePaymentProvider>();
        services.AddScoped<PaymentService>();

        // Address
        services.AddScoped<IAddressService, AddressService>();
        services.AddScoped<IAddressRepository, AddressRepository>();

        // AppFeedback
        services.AddScoped<IAppFeedbackService, AppFeedbackService>();
        services.AddScoped<IAppFeedbackRepository, AppFeedbackRepository>();

        // Booking
        services.AddScoped<IBookingService, BookingService>();
        services.AddScoped<IBookingRepository, BookingRepository>();

        // Company
        services.AddScoped<ICompanyService, CompanyService>();
        services.AddScoped<ICompanyRepository, CompanyRepository>();

        // Country
        services.AddScoped<ICountryService, CountryService>();
        services.AddScoped<ICountryRepository, CountryRepository>();

        // District
        services.AddScoped<IDistrictService, DistrictService>();
        services.AddScoped<IDistrictRepository, DistrictRepository>();

        // Favorite
        services.AddScoped<IFavoriteService, FavoriteService>();
        services.AddScoped<IFavoriteRepository, FavoriteRepository>();

        // FuelPrice
        services.AddScoped<IFuelPriceService, FuelPriceService>();
        services.AddScoped<IFuelPriceRepository, FuelPriceRepository>();

        // FuelStation
        services.AddScoped<IFuelStationService, FuelStationService>();
        services.AddScoped<IFuelStationRepository, FuelStationRepository>();

        // FuelSubType
        services.AddScoped<IFuelSubTypeService, FuelSubTypeService>();
        services.AddScoped<IFuelSubTypeRepository, FuelSubTypeRepository>();

        // FuelType
        services.AddScoped<IFuelTypeService, FuelTypeService>();
        services.AddScoped<IFuelTypeRepository, FuelTypeRepository>();

        // News
        services.AddScoped<INewsService, NewsService>();
        services.AddScoped<INewsRepository, NewsRepository>();

        // Notification
        services.AddScoped<INotificationService, NotificationService>();
        services.AddScoped<INotificationRepository, NotificationRepository>();

        // Parking
        services.AddScoped<IParkingService, ParkingService>();
        services.AddScoped<IParkingRepository, ParkingRepository>();

        // Region
        services.AddScoped<IRegionService, RegionService>();
        services.AddScoped<IRegionRepository, RegionRepository>();

        // ServiceCenter
        services.AddScoped<IServiceCenterService, ServiceCenterService>();
        services.AddScoped<IServiceCenterRepository, ServiceCenterRepository>();

        // User
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IUserRepository, UserRepository>();

        // Vehicle
        services.AddScoped<IVehicleService, VehicleService>();
        services.AddScoped<IVehicleRepository, VehicleRepository>();

        // Mapping
        services.AddScoped<IMappingService, MappingService>();
        services.RegisterMappingProfiles();

        return services;
    }
}
