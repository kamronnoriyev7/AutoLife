using AutoLife.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoLife.Persistence.Repositories.ServiceCenterRepositories;

internal class ServiceCenterRepository : GenericRepository<ServiceCenter>, IServiceCenterRepository
{
    public ServiceCenterRepository(DbContext context) : base(context)
    {
    }
    public async Task<List<ServiceCenter>> GetAllWithDistrictAsync(CancellationToken cancellationToken = default)
    {
        return await _context.Set<ServiceCenter>()
            .Include(sc => sc.Address.District)
            .Where(sc => !sc.IsDeleted)
            .ToListAsync(cancellationToken);
    }
}

