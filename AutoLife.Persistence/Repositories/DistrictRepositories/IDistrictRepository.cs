using AutoLife.Domain.Entities;

namespace AutoLife.Persistence.Repositories.DistrictRepositories;

public interface IDistrictRepository : IGenericRepository<District>
{
    Task<List<District>> GetAllWithRegionAsync(CancellationToken cancellationToken = default);
}