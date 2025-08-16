using AutoLife.Domain.Entities;
using AutoLife.Persistence.DataBaseContext;

namespace AutoLife.Persistence.Repositories.VehicleRepositories;

public interface IVehicleRepository : IGenericRepository<Vehicle, AppDbContext>
{
}