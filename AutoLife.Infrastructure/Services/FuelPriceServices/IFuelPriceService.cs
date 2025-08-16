using AutoLife.Application.DTOs.FuelPriceDTOs;
using AutoLife.Domain.Entities;

namespace AutoLife.Infrastructure.Services.FuelPriceServices;

public interface IFuelPriceService
{
    Task<FuelPrice> AddFuelPriceAsync(FuelPriceCreateDto fuelPriceDto);
    Task<FuelPrice> UpdateFuelPriceAsync(Guid id, FuelPriceCreateDto fuelPriceDto);
    Task DeleteFuelPriceAsync(Guid id);
    Task<FuelPrice> GetFuelPriceByIdAsync(Guid id);
    Task<IEnumerable<FuelPrice>> GetAllFuelPricesAsync();
    Task<IEnumerable<FuelPrice>> GetFuelPricesByFuelSubTypeIdAsync(Guid fuelSubTypeId);
}