using AutoLife.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoLife.Persistence.Repositories.DistrictRepositories;

public class DistrictRepository : GenericRepository<District> ,IDistrictRepository
{
    public DistrictRepository(DbContext context) : base(context)
    {
    }

    public async Task<List<District>> GetAllWithRegionAsync(CancellationToken cancellationToken = default)
    {
        return await _context.Set<District>()
            .Include(d => d.Region)
            .Where(d => !d.IsDeleted)
            .ToListAsync(cancellationToken);
    }
}
