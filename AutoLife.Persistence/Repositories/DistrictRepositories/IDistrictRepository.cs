using AutoLife.Domain.Entities;
using AutoLife.Persistence.DataBaseContext;

namespace AutoLife.Persistence.Repositories.DistrictRepositories;

public interface IDistrictRepository : IGenericRepository<District, AppDbContext>
{
    Task<List<District>> GetAllWithRegionAsync(CancellationToken cancellationToken = default);
}