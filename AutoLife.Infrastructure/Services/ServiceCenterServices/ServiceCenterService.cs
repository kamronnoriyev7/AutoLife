using AutoLife.Application.DTOs.ServiceCentersDTOs;
using AutoLife.Domain.Entities;
using AutoLife.Persistence.DataBaseContext;
using AutoLife.Persistence.Repositories;
using AutoLife.Persistence.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoLife.Infrastructure.Services.ServiceCenterServices;

public class ServiceCenterService : IServiceCenterService
{
    private readonly IUnitOfWork<AppDbContext> _unitOfWork;
    private readonly IGenericRepository<ServiceCenter> _serviceCenterRepository;

    public ServiceCenterService(IUnitOfWork<AppDbContext> unitOfWork, IGenericRepository<ServiceCenter> serviceCenterRepository)
    {
        _unitOfWork = unitOfWork;
        _serviceCenterRepository = serviceCenterRepository;
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

    public async Task<IEnumerable<ServiceCenterResponseDto>> GetServiceCentersByLocationAsync(string location)
    {
        if (string.IsNullOrWhiteSpace(location))
            throw new ArgumentException("Location cannot be null or empty.", nameof(location));
        var serviceCenters = await _serviceCenterRepository.FindAsync(sc => !sc.IsDeleted && sc.Address != null && sc.Address.HouseNumber.Contains(location));
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
        _serviceCenterRepository.Update(serviceCenter);
        await _unitOfWork.SaveChangesAsync();
    }
}
