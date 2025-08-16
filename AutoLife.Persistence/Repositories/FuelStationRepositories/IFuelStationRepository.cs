using AutoLife.Domain.Entities;
using AutoLife.Persistence.DataBaseContext;

namespace AutoLife.Persistence.Repositories.FuelStationRepositories;

public interface IFuelStationRepository : IGenericRepository<FuelStation, AppDbContext>
{
    Task<FuelStation?> GetWithDetailsAsync(Guid id, CancellationToken cancellationToken = default);
    Task<List<FuelStation>> GetAllWithDetailsAsync(CancellationToken cancellationToken = default);
    Task<IQueryable<FuelStation>> GetFuelStationsByLocationAsync(
        Guid? countryId = null,
        Guid? regionId = null,
        Guid? districtId = null,
        string? street = null, 
        CancellationToken cancellationToken = default);
}