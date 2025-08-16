using AutoLife.Domain.Entities;
using AutoLife.Persistence.DataBaseContext;

namespace AutoLife.Persistence.Repositories.FuelSubTypeRepositories;
public interface IFuelSubTypeRepository : IGenericRepository<FuelSubType, AppDbContext>
{
}