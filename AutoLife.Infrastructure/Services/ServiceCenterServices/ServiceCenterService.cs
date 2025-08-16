using AutoLife.Application.DTOs.ServiceCentersDTOs;
using AutoLife.Domain.Entities;
using AutoLife.Infrastructure.Mappers;
using AutoLife.Persistence.DataBaseContext;
using AutoLife.Persistence.Repositories;
using AutoLife.Persistence.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoLife.Infrastructure.Services.ServiceCenterServices;

public class ServiceCenterService : IServiceCenterService
{
    private readonly IUnitOfWork<AppDbContext> _unitOfWork;
    private readonly IGenericRepository<ServiceCenter, AppDbContext> _serviceCenterRepository;
    private readonly IMappingService _mappingService;

    public ServiceCenterService(IUnitOfWork<AppDbContext> unitOfWork, IGenericRepository<ServiceCenter, AppDbContext> serviceCenterRepository, IMappingService mappingService)
    {
        _unitOfWork = unitOfWork;
        _serviceCenterRepository = serviceCenterRepository;
        _mappingService = mappingService;
    }

    public async Task AddServiceCenterAsync(CreateServiceCenterDto serviceCenterDto)
    {
        if (serviceCenterDto == null)
            throw new ArgumentNullException(nameof(serviceCenterDto), "Service center cannot be null.");
        var serviceCenter = new ServiceCenter
        {
            Id = Guid.NewGuid(),
            Name = serviceCenterDto.Name,
            AddressId = serviceCenterDto.AddressId,
            PhoneNumber = serviceCenterDto.PhoneNumber,
            Description = serviceCenterDto.Description,
            CompanyId = serviceCenterDto.CompanyId,
            ServiceType = serviceCenterDto.ServiceType,
            UserId = serviceCenterDto.UserId,
        };
        await _serviceCenterRepository.AddAsync(serviceCenter);
        await _unitOfWork.SaveChangesAsync();
    }

    public async Task DeleteServiceCenterAsync(Guid id)
    {
        if (id == Guid.Empty)
            throw new ArgumentException("Invalid service center ID", nameof(id));
        var serviceCenter = await _serviceCenterRepository.GetByIdAsync(id, asNoTracking: true);
        if (serviceCenter == null)
            throw new KeyNotFoundException($"Service center with ID {id} not found.");

        serviceCenter.IsDeleted = true; // Soft delete
        serviceCenter.DeleteDate = DateTime.UtcNow;

        await _serviceCenterRepository.SoftDeleteAsync(serviceCenter.Id);
        await _unitOfWork.SaveChangesAsync();
    }

    public async Task<IEnumerable<ServiceCenterResponseDto>> GetAllServiceCentersAsync()
    {
        var serviceCenters = await _serviceCenterRepository.FindAsync(sc => !sc.IsDeleted);
        return serviceCenters.Select(sc => new ServiceCenterResponseDto
        {
            Id = sc.Id,
            Name = sc.Name,
            AddressId = sc.AddressId,
            PhoneNumber = sc.PhoneNumber,
            AddressText = sc.Address?.HouseNumber ?? string.Empty,
            Description = sc.Description ?? string.Empty,
            CompanyId = sc.CompanyId,
            ServiceType = sc.ServiceType,
            UserId = sc.UserId,
        });
    }

    public async Task<ServiceCenterResponseDto> GetServiceCenterByIdAsync(Guid id)
    {
        if (id == Guid.Empty)
            throw new ArgumentException("Invalid service center ID", nameof(id));
        var serviceCenter = await _serviceCenterRepository.GetByIdAsync(id, asNoTracking: true);
        if (serviceCenter == null)
            throw new KeyNotFoundException($"Service center with ID {id} not found.");
        return new ServiceCenterResponseDto
        {
            Id = serviceCenter.Id,
            Name = serviceCenter.Name,
            AddressId = serviceCenter.AddressId,
            PhoneNumber = serviceCenter.PhoneNumber,
            AddressText = serviceCenter.Address?.HouseNumber ?? string.Empty,
            Description = serviceCenter.Description ?? string.Empty,
            CompanyId = serviceCenter.CompanyId,
            ServiceType = serviceCenter.ServiceType,
            UserId = serviceCenter.UserId,
        };
    }

    public async Task<IEnumerable<ServiceCenterResponseDto>> GetServiceCentersByLocationAsync(string locationName)
    {
        if (string.IsNullOrWhiteSpace(locationName))
            throw new ArgumentException("Location cannot be null or empty.", nameof(locationName));
        var serviceCenters = await _serviceCenterRepository.FindAsync(sc => !sc.IsDeleted && sc.Address != null && sc.Address.HouseNumber.Contains(locationName));
        return serviceCenters.Select(sc => new ServiceCenterResponseDto
        {
            Id = sc.Id,
            Name = sc.Name,
            AddressId = sc.AddressId,
            PhoneNumber = sc.PhoneNumber,
            AddressText = sc.Address?.HouseNumber ?? string.Empty,
            Description = sc.Description ?? string.Empty,
            CompanyId = sc.CompanyId,
            ServiceType = sc.ServiceType,
            UserId = sc.UserId,
        });
    }

    public async Task<ICollection<ServiceCenterResponseDto>> GetServiceCentersByNearbyLocationAsync(
         double latitude,
         double longitude,
         double radiusKm,
         CancellationToken cancellationToken = default)
    {
        if (radiusKm <= 0)
            throw new ArgumentException("Radius must be greater than zero.", nameof(radiusKm));

        if (latitude < -90 || latitude > 90 || longitude < -180 || longitude > 180)
            throw new ArgumentException("Latitude and Longitude must be valid coordinates.");

        const string sql = @"
        SELECT sc.Id
        FROM ServiceCenters sc
        JOIN Addresses a ON sc.AddressId = a.Id
        JOIN GeoLocations g ON a.GeoLocationId = g.Id
        WHERE 
            sc.IsDeleted = 0 AND
            a.IsDeleted = 0 AND
            g.IsDeleted = 0 AND
            (
                6371 * 2 * ASIN(SQRT(
                    POWER(SIN(RADIANS(g.Latitude - @p0) / 2), 2) +
                    COS(RADIANS(@p0)) * COS(RADIANS(g.Latitude)) *
                    POWER(SIN(RADIANS(g.Longitude - @p1) / 2), 2)
                ))
            ) <= @p2
        ";

        var rawIds = await _serviceCenterRepository
            .FromSqlRawAsync<ServiceCenterIdResult>(sql, latitude, longitude, radiusKm);

        if (rawIds == null || !rawIds.Any())
            return new List<ServiceCenterResponseDto>();

        var serviceCenterIds = rawIds.Select(r => r.Id).ToList();

        if (!serviceCenterIds.Any())
            return new List<ServiceCenterResponseDto>();

        var serviceCenters = await _serviceCenterRepository
            .GetQueryable()
            .Include(sc => sc.Address)
            .ThenInclude(a => a.GeoLocation)
            .Where(sc => serviceCenterIds.Contains(sc.Id)
                      && !sc.IsDeleted
                      && sc.Address != null
                      && sc.Address.GeoLocation != null)
            .ToListAsync(cancellationToken);

        return _mappingService.Map<ICollection<ServiceCenterResponseDto>>(serviceCenters);
    }

    public async Task<IEnumerable<ServiceCenterResponseDto>> GetServiceCentersByTypeAsync(string type)
    {
        if (string.IsNullOrWhiteSpace(type))
            throw new ArgumentException("Type cannot be null or empty.", nameof(type));
        var serviceCenters = await _serviceCenterRepository.FindAsync(sc => !sc.IsDeleted && sc.ServiceType.Equals(type));
        return serviceCenters.Select(sc => new ServiceCenterResponseDto
        {
            Id = sc.Id,
            Name = sc.Name,
            AddressId = sc.AddressId,
            PhoneNumber = sc.PhoneNumber,
            AddressText = sc.Address?.HouseNumber ?? string.Empty,
            Description = sc.Description ?? string.Empty,
            CompanyId = sc.CompanyId,
            ServiceType = sc.ServiceType,
            UserId = sc.UserId,
        });
    }

    public async Task UpdateServiceCenterAsync(Guid id, CreateServiceCenterDto serviceCenterDto)
    {
        if (id == Guid.Empty)
            throw new ArgumentException("Invalid service center ID", nameof(id));
        if (serviceCenterDto == null)
            throw new ArgumentNullException(nameof(serviceCenterDto), "Service center cannot be null.");
        var serviceCenter = await _serviceCenterRepository.GetByIdAsync(id, asNoTracking: true);
        if (serviceCenter == null)
            throw new KeyNotFoundException($"Service center with ID {id} not found.");

        serviceCenter.Name = serviceCenterDto.Name;
        serviceCenter.AddressId = serviceCenterDto.AddressId;
        serviceCenter.PhoneNumber = serviceCenterDto.PhoneNumber;
        serviceCenter.Description = serviceCenterDto.Description;
        serviceCenter.CompanyId = serviceCenterDto.CompanyId;
        serviceCenter.ServiceType = serviceCenterDto.ServiceType;
        serviceCenter.UserId = serviceCenterDto.UserId;
        serviceCenter.UpdateDate = DateTime.UtcNow;

        _serviceCenterRepository.Update(serviceCenter);
        await _unitOfWork.SaveChangesAsync();
    }
}
