using AutoLife.Domain.Entities;

namespace AutoLife.Persistence.Repositories.FuelStationRepositories;

public interface IFuelStationRepository : IGenericRepository<FuelStation>
{
    Task<FuelStation?> GetWithDetailsAsync(Guid id, CancellationToken cancellationToken = default);
    Task<List<FuelStation>> GetAllWithDetailsAsync(CancellationToken cancellationToken = default);
}