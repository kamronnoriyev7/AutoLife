using AutoLife.Domain.Entities;
using AutoLife.Persistence.DataBaseContext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoLife.Persistence.Repositories.CompanyRepositories;

public class CompanyRepository : GenericRepository<Company, AppDbContext>, ICompanyRepository
{
    public CompanyRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<Company?> GetWithAllDetailsAsync(Guid id)
    {
        return await _context.Set<Company>()
            .Include(c => c.Address)
            .Include(c => c.ServiceCenters)
            .Include(c => c.FuelStations)
            .Include(c => c.Notifications)
            .Include(c => c.Parkings)
            .Include(c => c.Ratings)
            .Include(c => c.Vehicles)
            .Include(c => c.Images)
            .Include(c => c.NewsList)
            .FirstOrDefaultAsync(c => c.Id == id);
    }
}
