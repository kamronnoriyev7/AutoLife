using AutoLife.Application.DTOs.VehicleDTOs;

namespace AutoLife.Infrastructure.Services.VehicleServices;

public interface IVehicleService
{
    Task AddVehicleAsync(VehicleCreateDto vehicleDto);
    Task UpdateVehicleAsync(Guid id, VehicleCreateDto vehicleDto);
    Task DeleteVehicleAsync(Guid id);
    Task<VehicleResponseDto> GetVehicleByIdAsync(Guid id);
    Task<IEnumerable<VehicleResponseDto>> GetAllVehiclesAsync();
    Task<IEnumerable<VehicleResponseDto>> GetVehiclesByUserIdAsync(Guid userId);
    Task<IEnumerable<VehicleResponseDto>> GetVehiclesByMakeAsync(string make);
    Task<IEnumerable<VehicleResponseDto>> GetVehiclesByModelAsync(string model);
}