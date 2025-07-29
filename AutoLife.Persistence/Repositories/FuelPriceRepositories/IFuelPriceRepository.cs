using AutoLife.Domain.Entities;

namespace AutoLife.Persistence.Repositories.FuelPriceRepositories;


public interface IFuelPriceRepository : IGenericRepository<FuelPrice>
{
    Task<IEnumerable<FuelPrice>> GetPricesByStationIdAsync(Guid stationId);
    Task<IEnumerable<FuelPrice>> GetPricesByFuelSubTypeIdAsync(Guid fuelSubTypeId);
}