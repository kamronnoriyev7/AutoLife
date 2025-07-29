using AutoLife.Application.DTOs.FuelStationsDTOs;
using AutoLife.Domain.Entities;
using AutoLife.Persistence.DataBaseContext;
using AutoLife.Persistence.Repositories;
using AutoLife.Persistence.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoLife.Infrastructure.Services.FuelStationServices;

public class FuelStationService : IFuelStationService
{
    private readonly IUnitOfWork<AppDbContext> _unitOfWork;
    private readonly IGenericRepository<FuelStation> _fuelStationRepository;

    public FuelStationService(IUnitOfWork<AppDbContext> unitOfWork, IGenericRepository<FuelStation> fuelStationRepository)
    {
        _unitOfWork = unitOfWork;
        _fuelStationRepository = fuelStationRepository;
    }

    public async Task AddFuelStationAsync(CreateFuelStationDto fuelStationDto)
    {
        if (fuelStationDto == null)
            throw new ArgumentNullException(nameof(fuelStationDto), "Fuel station cannot be null.");
        var fuelStation = new FuelStation
        {
            Id = Guid.NewGuid(),
            Name = fuelStationDto.Name,
            AddressId = fuelStationDto.AddressId,
            CompanyId = fuelStationDto.CompanyId,
            FuelTypeId = fuelStationDto.FuelTypeId,
            FuelSubTypeId = fuelStationDto.FuelSubTypeId,
            UserId = fuelStationDto.UserId,
            OperatorName = fuelStationDto.OperatorName,
            PhoneNumber = fuelStationDto.PhoneNumber,
            CreateDate = DateTime.UtcNow,
        };

        await _fuelStationRepository.AddAsync(fuelStation);
        await _unitOfWork.SaveChangesAsync();
    }

    public async Task DeleteFuelStationAsync(Guid id)
    {
        if (id == Guid.Empty)
            throw new ArgumentException("Invalid fuel station ID", nameof(id));

        var fuelStation = await _fuelStationRepository.GetByIdAsync(id, asNoTracking: true);
        if (fuelStation == null)
            throw new KeyNotFoundException($"Fuel station with ID {id} not found.");

        fuelStation.IsDeleted = true; 
        fuelStation.DeleteDate = DateTime.UtcNow;

        await _fuelStationRepository.SoftDeleteAsync(fuelStation.Id);
        await _unitOfWork.SaveChangesAsync();
    }

    public async Task<IEnumerable<FuelStationResponseDto>> GetAllFuelStationsAsync()
    {
        var fuelStations = await _fuelStationRepository.FindAsync(fs => !fs.IsDeleted);
        return fuelStations.Select(fs => new FuelStationResponseDto
        {
            Id = fs.Id,
            Name = fs.Name,
            AddressId = fs.AddressId,
            CompanyId = fs.CompanyId,
            FuelTypeId = fs.FuelTypeId,
            FuelSubTypeId = fs.FuelSubTypeId,
            UserId = fs.UserId,
            OperatorName = fs.OperatorName,
            PhoneNumber = fs.PhoneNumber,
        });
    }

    public async Task<FuelStationResponseDto> GetFuelStationByIdAsync(Guid id)
    {
        if (id == Guid.Empty)
            throw new ArgumentException("Invalid fuel station ID", nameof(id));

        var fuelStation = await _fuelStationRepository.GetByIdAsync(id, asNoTracking: true);
        if (fuelStation == null || fuelStation.IsDeleted)
            throw new KeyNotFoundException($"Fuel station with ID {id} not found.");

        return new FuelStationResponseDto
        {
            Id = fuelStation.Id,
            Name = fuelStation.Name,
            AddressId = fuelStation.AddressId,
            CompanyId = fuelStation.CompanyId,
            FuelTypeId = fuelStation.FuelTypeId,
            FuelSubTypeId = fuelStation.FuelSubTypeId,
            UserId = fuelStation.UserId,
            OperatorName = fuelStation.OperatorName,
            PhoneNumber = fuelStation.PhoneNumber,
        };
    }

    public async Task<IEnumerable<FuelStationResponseDto>> GetFuelStationsByFuelTypeAsync(FuelType fuelType)
    {
        if (fuelType == null)
            throw new ArgumentNullException(nameof(fuelType), "Fuel type cannot be null.");

        var fuelStations = await _fuelStationRepository.FindAsync(fs => fs.FuelTypeId == fuelType.Id && !fs.IsDeleted);

        return fuelStations.Select(fs => new FuelStationResponseDto
        {
            Id = fs.Id,
            Name = fs.Name,
            AddressId = fs.AddressId,
            CompanyId = fs.CompanyId,
            FuelTypeId = fs.FuelTypeId,
            FuelSubTypeId = fs.FuelSubTypeId,
            UserId = fs.UserId,
            OperatorName = fs.OperatorName,
            PhoneNumber = fs.PhoneNumber,
        });
    }

    public async Task<IEnumerable<FuelStationResponseDto>> GetFuelStationsByLocationAsync(GeoLocation location)
    {
        if (location == null)
            throw new ArgumentNullException(nameof(location), "Location cannot be null.");

        var fuelStations = await _fuelStationRepository.FindAsync(fs => fs.Address.GeoLocation.Latitude == location.Latitude && fs.Address.GeoLocation.Longitude == location.Longitude && !fs.IsDeleted);

        return fuelStations.Select(fs => new FuelStationResponseDto
        {
            Id = fs.Id,
            Name = fs.Name,
            AddressId = fs.AddressId,
            CompanyId = fs.CompanyId,
            FuelTypeId = fs.FuelTypeId,
            FuelSubTypeId = fs.FuelSubTypeId,
            UserId = fs.UserId,
            OperatorName = fs.OperatorName,
            PhoneNumber = fs.PhoneNumber,
        });
    }

    public async Task<FuelStationResponseDto> UpdateFuelStationAsync(Guid id, UpdateFuelStationDto fuelStationDto)
    {
        if (id == Guid.Empty)
            throw new ArgumentException("Invalid fuel station ID", nameof(id));

        if (fuelStationDto == null)
            throw new ArgumentNullException(nameof(fuelStationDto), "Fuel station cannot be null.");

        var fuelStation = await _fuelStationRepository.GetByIdAsync(id, asNoTracking: true);
        if (fuelStation == null || fuelStation.IsDeleted)
            throw new KeyNotFoundException($"Fuel station with ID {id} not found.");

        fuelStation.Name = fuelStationDto.Name;
        fuelStation.AddressId = fuelStationDto.AddressId;
        fuelStation.CompanyId = fuelStationDto.CompanyId;
        fuelStation.FuelTypeId = fuelStationDto.FuelTypeId;
        fuelStation.FuelSubTypeId = fuelStationDto.FuelSubTypeId;
        fuelStation.UserId = fuelStationDto.UserId;
        fuelStation.OperatorName = fuelStationDto.OperatorName;
        fuelStation.PhoneNumber = fuelStationDto.PhoneNumber;

        _fuelStationRepository.Update(fuelStation);
        await _unitOfWork.SaveChangesAsync();

        return new FuelStationResponseDto
        {
            Id = fuelStation.Id,
            Name = fuelStation.Name,
            AddressId = fuelStation.AddressId,
            CompanyId = fuelStation.CompanyId,
            FuelTypeId = fuelStation.FuelTypeId,
            FuelSubTypeId = fuelStation.FuelSubTypeId,
            UserId = fuelStation.UserId,
            OperatorName = fuelStation.OperatorName,
            PhoneNumber = fuelStation.PhoneNumber,
        };
    }
}
