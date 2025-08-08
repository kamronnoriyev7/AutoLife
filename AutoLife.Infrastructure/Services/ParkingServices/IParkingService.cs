using AutoLife.Application.DTOs.ParkingDTOs;
using AutoLife.Domain.Entities;

namespace AutoLife.Infrastructure.Services.ParkingServices;

public interface IParkingService
{
    Task AddParkingAsync(ParkingCreateDto parkingDto);
    Task UpdateParkingAsync(Guid id, ParkingCreateDto parkingDto);
    Task DeleteParkingAsync(Guid id);
    Task<Parking> GetParkingByIdAsync(Guid id);
    Task<IEnumerable<Parking>> GetAllParkingsAsync();
    Task<IEnumerable<Parking>> GetParkingsByLocationAsync(Country country);
    Task<IEnumerable<Parking>> GetParkingsByTypeAsync(bool type);
    Task<ICollection<Parking>> GetParkingsByNearbyLocationAsync(
        double latitude,
        double longitude,
        double radiusKm,
        CancellationToken cancellationToken = default);
}