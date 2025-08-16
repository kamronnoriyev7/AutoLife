using AutoLife.Application.DTOs.FuelStationsDTOs;
using AutoLife.Domain.Entities;
using AutoLife.Infrastructure.Mappers;
using AutoLife.Persistence.DataBaseContext;
using AutoLife.Persistence.Repositories;
using AutoLife.Persistence.UnitOfWork;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Reflection.Metadata.Ecma335;

namespace AutoLife.Infrastructure.Services.FuelStationServices;

public class FuelStationService : IFuelStationService
{
    private readonly IUnitOfWork<AppDbContext> _unitOfWork;
    private readonly IGenericRepository<FuelStation, AppDbContext> _fuelStationRepository;
    private readonly IMappingService _mappingService;

    public FuelStationService(IUnitOfWork<AppDbContext> unitOfWork, IGenericRepository<FuelStation, AppDbContext> fuelStationRepository, IMappingService mappingService)
    {
        _unitOfWork = unitOfWork;
        _fuelStationRepository = fuelStationRepository;
        _mappingService = mappingService;
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
            UserId = fuelStation.UserId,
            OperatorName = fuelStation.OperatorName,
            PhoneNumber = fuelStation.PhoneNumber,
        };
    }

  

    public async Task<IEnumerable<FuelStationResponseDto>> GetFuelStationsByLocationAsync(GeoLocation location)
    {
        if (location == null)
            throw new ArgumentNullException(nameof(location), "Location cannot be null.");

        var fuelStations = await _fuelStationRepository.FindAsync(
            fs => fs.Address != null
               && fs.Address.GeoLocation != null
               && fs.Address.GeoLocation.Latitude == location.Latitude
               && fs.Address.GeoLocation.Longitude == location.Longitude
               && !fs.IsDeleted
        );


        return _mappingService.Map<IEnumerable<FuelStationResponseDto>>(fuelStations);
    }

    public async Task<ICollection<FuelStationResponseDto>> GetFuelStationsByNearbyLocationAsync(
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
            SELECT fs.Id
            FROM FuelStations fs
            JOIN Addresses a ON fs.AddressId = a.Id
            JOIN GeoLocations g ON a.GeoLocationId = g.Id
            WHERE 
                fs.IsDeleted = 0 AND
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

        var rawIds = await _fuelStationRepository
            .FromSqlRawAsync<FuelStationIdResult>(sql, latitude, longitude, radiusKm);

        if (rawIds == null || !rawIds.Any())
            return new List<FuelStationResponseDto>();

        var stationIds = rawIds.Select(r => r.Id).ToList();

        if (!stationIds.Any())
            return new List<FuelStationResponseDto>();

        var fuelStations = await _fuelStationRepository
            .GetQueryable()
            .Include(fs => fs.Address)
            .ThenInclude(a => a.GeoLocation)
            .Where(fs => stationIds.Contains(fs.Id) && !fs.IsDeleted)
            .ToListAsync(cancellationToken);

        return _mappingService.Map<ICollection<FuelStationResponseDto>>(fuelStations);
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
            UserId = fuelStation.UserId,
            OperatorName = fuelStation.OperatorName,
            PhoneNumber = fuelStation.PhoneNumber,
        };
    }
}
