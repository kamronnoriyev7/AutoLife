using AutoLife.Application.Helpers;
using AutoLife.Domain.Entities;
using AutoLife.Persistence.DataBaseContext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoLife.Persistence.Repositories.FuelStationRepositories;

public class FuelStationRepository : GenericRepository<FuelStation, AppDbContext>, IFuelStationRepository
{
    public FuelStationRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<List<FuelStation>> GetAllWithDetailsAsync(CancellationToken cancellationToken = default)
    {
        return await _context.Set<FuelStation>()
            .Include(fs => fs.Address)
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

    public async Task<IQueryable<FuelStation>> GetFuelStationsByLocationAsync(
       Guid? countryId = null,
       Guid? regionId = null,
       Guid? districtId = null,
       string? street = null,
       double? latitude = null,
       double? longitude = null,
       double? radiusKm = null,
       CancellationToken cancellationToken = default)
    {
        var query = _context.FuelStations.AsQueryable();

        if (latitude.HasValue && longitude.HasValue && radiusKm.HasValue)
        {
            query = query
                .AsEnumerable()
                .Where(f =>
                    f.Address != null &&
                    f.Address.GeoLocation != null &&
                    GeoDistanceHelper.CalculateDistance(
                        latitude.Value, longitude.Value,
                        f.Address.GeoLocation.Latitude,
                        f.Address.GeoLocation.Longitude) <= radiusKm.Value)
                .AsQueryable();
        }

        return await Task.FromResult(query);
    }



    public async Task<FuelStation?> GetWithDetailsAsync(Guid id, CancellationToken cancellationToken = default)
    {
        if (id == Guid.Empty)
            throw new ArgumentException("Invalid fuel station ID", nameof(id));

        return await _context.Set<FuelStation>()
            .Include(fs => fs.Address)
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
