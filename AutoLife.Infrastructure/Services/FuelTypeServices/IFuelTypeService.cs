using AutoLife.Application.DTOs.FuelTypeDTOs;
using AutoLife.Domain.Entities;

namespace AutoLife.Infrastructure.Services.FuelTypeServices;

public interface IFuelTypeService
{
    Task<FuelType> AddFuelTypeAsync(FuelTypeCreateDto fuelTypeDto);
    Task<FuelType> UpdateFuelTypeAsync(Guid id, FuelTypeCreateDto fuelTypeDto);
    Task DeleteFuelTypeAsync(Guid id);
    Task<FuelType> GetFuelTypeByIdAsync(Guid id);
    Task<IEnumerable<FuelType>> GetAllFuelTypesAsync();
    Task<IEnumerable<FuelType>> GetFuelTypesByStationIdAsync(Guid stationId);
}