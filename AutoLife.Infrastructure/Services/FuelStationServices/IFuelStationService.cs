using AutoLife.Application.DTOs.FuelStationsDTOs;
using AutoLife.Domain.Entities;

namespace AutoLife.Infrastructure.Services.FuelStationServices;

public interface IFuelStationService
{
    Task AddFuelStationAsync(CreateFuelStationDto fuelStationDto);
    Task<FuelStationResponseDto> UpdateFuelStationAsync(Guid id, UpdateFuelStationDto fuelStationDto);
    Task DeleteFuelStationAsync(Guid id);
    Task<FuelStationResponseDto> GetFuelStationByIdAsync(Guid id);
    Task<IEnumerable<FuelStationResponseDto>> GetAllFuelStationsAsync();
    Task<IEnumerable<FuelStationResponseDto>> GetFuelStationsByLocationAsync(GeoLocation location);
    Task<ICollection<FuelStationResponseDto>> GetFuelStationsByNearbyLocationAsync(
            double latitude,
            double longitude,
            double radiusKm,
            CancellationToken cancellationToken = default);
}