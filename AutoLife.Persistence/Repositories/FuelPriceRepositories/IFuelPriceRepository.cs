using AutoLife.Domain.Entities;
using AutoLife.Persistence.DataBaseContext;

namespace AutoLife.Persistence.Repositories.FuelPriceRepositories;


public interface IFuelPriceRepository : IGenericRepository<FuelPrice, AppDbContext>
{
    Task<IEnumerable<FuelPrice>> GetPricesByFuelSubTypeIdAsync(Guid fuelSubTypeId);
}