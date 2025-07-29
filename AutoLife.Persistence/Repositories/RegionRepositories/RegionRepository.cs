using AutoLife.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoLife.Persistence.Repositories.RegionRepositories;

public class RegionRepository : GenericRepository<Region>, IRegionRepository
{
    public RegionRepository(DbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Region>> GetRegionsByCountryIdAsync(Guid countryId)
    {
        if (countryId == Guid.Empty)
            throw new ArgumentException("Country ID cannot be empty.", nameof(countryId));
        return await _context.Set<Region>()
            .Where(r => r.CountryId == countryId && !r.IsDeleted)
            .ToListAsync();
    }

    public async Task<IEnumerable<Region>> GetRegionsByDistrictIdAsync(Guid districtId)
    {
        if (districtId == Guid.Empty)
            throw new ArgumentException("District ID cannot be empty.", nameof(districtId));
        
        return await _context.Set<Region>()
            .Where(r => r.Districts.Any(d => d.BasaEntityId == districtId) && !r.IsDeleted)
            .ToListAsync();
    }
}
