using AutoLife.Domain.Entities;
using AutoLife.Persistence.DataBaseContext;

namespace AutoLife.Persistence.Repositories.FuelTypeRepositories;

public interface IFuelTypeRepository : IGenericRepository<FuelType, AppDbContext>
{
    Task<List<FuelType>> GetAllWithSubTypesAsync(CancellationToken cancellationToken = default);
    Task<FuelType?> GetByIdWithSubTypesAsync(Guid id, CancellationToken cancellationToken = default);
    Task<bool> IsFuelTypeNameUniqueAsync(string name, Guid? id = null, CancellationToken cancellationToken = default);
}