using AutoLife.Domain.Entities;
using AutoLife.Persistence.DataBaseContext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoLife.Persistence.Repositories.ServiceCenterRepositories;

public class ServiceCenterRepository(AppDbContext context) : GenericRepository<ServiceCenter,AppDbContext>(context), IServiceCenterRepository
{
    public async Task<List<ServiceCenter>> GetAllWithDistrictAsync(CancellationToken cancellationToken = default)
    {
        return await _context.Set<ServiceCenter>()
            .Include(sc => sc.Address.District)
            .Where(sc => !sc.IsDeleted)
            .ToListAsync(cancellationToken);
    }
}

