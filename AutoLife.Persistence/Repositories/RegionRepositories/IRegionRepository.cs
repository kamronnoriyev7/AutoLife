using AutoLife.Domain.Entities;

namespace AutoLife.Persistence.Repositories.RegionRepositories;

public interface IRegionRepository : IGenericRepository<Region>
{
    Task<IEnumerable<Region>> GetRegionsByCountryIdAsync(Guid countryId);
    Task<IEnumerable<Region>> GetRegionsByDistrictIdAsync(Guid districtId);
}
