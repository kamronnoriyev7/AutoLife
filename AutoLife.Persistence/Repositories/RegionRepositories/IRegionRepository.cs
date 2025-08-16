using AutoLife.Domain.Entities;
using AutoLife.Persistence.DataBaseContext;

namespace AutoLife.Persistence.Repositories.RegionRepositories;

public interface IRegionRepository : IGenericRepository<Region, AppDbContext>
{
    Task<IEnumerable<Region>> GetRegionsByCountryIdAsync(Guid countryId);
    Task<IEnumerable<Region>> GetRegionsByDistrictIdAsync(Guid districtId);
}
