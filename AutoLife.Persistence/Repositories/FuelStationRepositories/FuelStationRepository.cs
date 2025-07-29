using AutoLife.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoLife.Persistence.Repositories.FuelStationRepositories;

public class FuelStationRepository : GenericRepository<FuelStation>, IFuelStationRepository
{
    public FuelStationRepository(DbContext context) : base(context)
    {
    }

    public async Task<List<FuelStation>> GetAllWithDetailsAsync(CancellationToken cancellationToken = default)
    {
        return await _context.Set<FuelStation>()
            .Include(fs => fs.Address)
            .Include(fs => fs.FuelPrices)
            .Include(fs => fs.FuelSubType)
            .Include(fs => fs.FuelType)
            .Include(fs => fs.Images)
            .Include(fs => fs.Ratings)
            .Include(fs => fs.Favorites)
            .Include(fs => fs.Notifications)
            .Include(fs => fs.News)
            .Include(fs => fs.Bookings)
            .Include(fs => fs.User) 
            .Where(fs => !fs.IsDeleted)
            .ToListAsync(cancellationToken);
    }

    public async Task<FuelStation?> GetWithDetailsAsync(Guid id, CancellationToken cancellationToken = default)
    {
        if (id == Guid.Empty)
            throw new ArgumentException("Invalid fuel station ID", nameof(id));

        return await _context.Set<FuelStation>()
            .Include(fs => fs.Address)
            .Include(fs => fs.FuelPrices)
            .Include(fs => fs.FuelSubType)
            .Include(fs => fs.FuelType)
            .Include(fs => fs.Images)
            .Include(fs => fs.Ratings)
            .Include(fs => fs.Favorites)
            .Include(fs => fs.Notifications)
            .Include(fs => fs.News)
            .Include(fs => fs.Bookings)
            .Include(fs => fs.User) 
            .FirstOrDefaultAsync(fs => fs.Id == id && !fs.IsDeleted, cancellationToken);
    }
}
