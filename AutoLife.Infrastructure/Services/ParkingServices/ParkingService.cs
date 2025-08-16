using AutoLife.Application.DTOs.ParkingDTOs;
using AutoLife.Domain.Entities;
using AutoLife.Persistence.DataBaseContext;
using AutoLife.Persistence.Repositories;
using AutoLife.Persistence.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoLife.Infrastructure.Services.ParkingServices;

public class ParkingService : IParkingService
{
    private readonly IUnitOfWork<AppDbContext> _unitOfWork;
    private readonly IGenericRepository<Parking, AppDbContext> _parkingRepository;

    public ParkingService(IUnitOfWork<AppDbContext> unitOfWork, IGenericRepository<Parking, AppDbContext> parkingRepository)
    {
        _unitOfWork = unitOfWork;
        _parkingRepository = parkingRepository;
    }

    public async Task AddParkingAsync(ParkingCreateDto parkingDto)
    {
        if (parkingDto == null)
            throw new ArgumentNullException(nameof(parkingDto), "Parking cannot be null.");
       
        var parking = new Parking
        {
            Id = Guid.NewGuid(),
            Name = parkingDto.Name,
            AddressId = parkingDto.AddressId,
            CompanyId = parkingDto.CompanyId,
            UserId = parkingDto.UserId,
            PhoneNumber = parkingDto.PhoneNumber,
            ClosingTime = parkingDto.ClosingTime,
            OpeningTime = parkingDto.OpeningTime,
            HourlyRate = parkingDto.HourlyRate,
            DailyRate = parkingDto.DailyRate,
            IsFree = parkingDto.IsFree,
            HasCameras = parkingDto.HasCameras,
            IsCovered = parkingDto.IsCovered,
            TotalSpaces = parkingDto.TotalSpaces,
            AvailableSpaces = parkingDto.AvailableSpaces,
            IsPreBookingAllowed = parkingDto.IsPreBookingAllowed,
        };

        await _parkingRepository.AddAsync(parking);
        await _unitOfWork.SaveChangesAsync();
    }

    public async Task DeleteParkingAsync(Guid id)
    {
        if (id == Guid.Empty)
            throw new ArgumentException("Invalid parking ID", nameof(id));
        var parking = await _parkingRepository.GetByIdAsync(id, asNoTracking: true);
        if (parking == null)
            throw new KeyNotFoundException($"Parking with ID {id} not found.");

        parking.IsDeleted = true; // Soft delete
        parking.DeleteDate = DateTime.UtcNow;
        await _parkingRepository.SoftDeleteAsync(parking.Id);
        await _unitOfWork.SaveChangesAsync();
    }

    public async Task<IEnumerable<Parking>> GetAllParkingsAsync()
    {
        var parkings = await _parkingRepository.FindAsync(p => !p.IsDeleted);
        if (parkings == null || !parkings.Any())
            throw new KeyNotFoundException("No parkings found.");
        return parkings;
    }

    public async Task<Parking> GetParkingByIdAsync(Guid id)
    {
        if (id == Guid.Empty)
            throw new ArgumentException("Invalid parking ID", nameof(id));
        var parking = await _parkingRepository.GetByIdAsync(id, asNoTracking: true);
        if (parking == null)
            throw new KeyNotFoundException($"Parking with ID {id} not found.");
        return parking;
    }

    public async Task<IEnumerable<Parking>> GetParkingsByLocationAsync(Country country)
    {
        if (country == null)
            throw new ArgumentNullException(nameof(country), "Country cannot be null.");

        var parkings = await _parkingRepository.FindAsync(
            p => p.Address != null &&
                 p.Address.Country != null &&
                 p.Address.Country.UzName == country.UzName &&
                 !p.IsDeleted
        );

        if (parkings == null || !parkings.Any())
            throw new KeyNotFoundException("No parkings found for the specified location.");

        return parkings;
    }


    public async Task<ICollection<Parking>> GetParkingsByNearbyLocationAsync(
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
        SELECT p.Id
        FROM Parkings p
        JOIN Addresses a ON p.AddressId = a.Id
        JOIN GeoLocations g ON a.GeoLocationId = g.Id
        WHERE 
            p.IsDeleted = 0 AND
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

        var rawIds = await _parkingRepository
            .FromSqlRawAsync<ParkingIdResult>(sql, latitude, longitude, radiusKm);

        if (rawIds == null || !rawIds.Any())
            return new List<Parking>();

        var parkingIds = rawIds.Select(r => r.Id).ToList();

        if (!parkingIds.Any())
            return new List<Parking>();

        var parkings = await _parkingRepository
            .GetQueryable()
            .Include(p => p.Address)
            .ThenInclude(a => a.GeoLocation)
            .Where(p => parkingIds.Contains(p.Id) && !p.IsDeleted)
            .ToListAsync(cancellationToken);

        return parkings;
    }


    public async Task<IEnumerable<Parking>> GetParkingsByTypeAsync(bool type)
    {
       if (type == false)
            throw new ArgumentNullException(nameof(type), "Parking type cannot be null.");

        var parkings = await _parkingRepository.FindAsync(p => p.IsFree == type && !p.IsDeleted);
        if (parkings == null || !parkings.Any())
            throw new KeyNotFoundException($"No parkings found for the type {type}.");
        
        return parkings;
    }

    public async Task UpdateParkingAsync(Guid id, ParkingCreateDto parkingDto)
    {
        if (id == Guid.Empty)
            throw new ArgumentException("Invalid parking ID", nameof(id));
        if (parkingDto == null)
            throw new ArgumentNullException(nameof(parkingDto), "Parking cannot be null.");

        var parking = await _parkingRepository.GetByIdAsync(id, asNoTracking: true);
        if (parking == null)
            throw new KeyNotFoundException($"Parking with ID {id} not found.");

        parking.Name = parkingDto.Name;
        parking.AddressId = parkingDto.AddressId;
        parking.CompanyId = parkingDto.CompanyId;
        parking.UserId = parkingDto.UserId;
        parking.PhoneNumber = parkingDto.PhoneNumber;
        parking.ClosingTime = parkingDto.ClosingTime;
        parking.OpeningTime = parkingDto.OpeningTime;
        parking.HourlyRate = parkingDto.HourlyRate;
        parking.DailyRate = parkingDto.DailyRate;
        parking.IsFree = parkingDto.IsFree;
        parking.HasCameras = parkingDto.HasCameras;
        parking.IsCovered = parkingDto.IsCovered;
        parking.TotalSpaces = parkingDto.TotalSpaces;
        parking.AvailableSpaces = parkingDto.AvailableSpaces;
        parking.IsPreBookingAllowed = parkingDto.IsPreBookingAllowed;
        parking.UpdateDate = DateTime.UtcNow;

        _parkingRepository.Update(parking);
        await _unitOfWork.SaveChangesAsync();
    }
}
