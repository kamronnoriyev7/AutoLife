using AutoLife.Domain.Entities;
using AutoLife.Persistence.DataBaseContext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoLife.Persistence.Repositories.ParkingRepositories;

public class ParkingRepository(AppDbContext context) : GenericRepository<Parking, AppDbContext>(context), IParkingRepository
{
    public async Task<IEnumerable<Parking>> GetAvailableParkingsAsync(DateTime startTime, DateTime endTime)
    {
        return await _context.Set<Parking>()
          .Include(p => p.Bookings)
          .Where(p => !p.Bookings!.Any(b =>
              (startTime < b.To && endTime > b.From)
          ) && p.AvailableSpaces > 0)
          .ToListAsync();
    }

    public async Task<Parking> GetParkingByIdAsync(Guid id, bool includeDeleted = false)
    {
        var query = _context.Set<Parking>()
              .Include(p => p.Address)
              .Include(p => p.Images)
              .Include(p => p.Ratings)
              .Include(p => p.News)
              .Include(p => p.Company);

        if (includeDeleted)
        {
            return await query.FirstOrDefaultAsync(p => p.Id == id)
                ?? throw new Exception("Parking topilmadi.");
        }
        else
        {
            return await query.FirstOrDefaultAsync(p => p.Id == id && !p.IsDeleted)
                ?? throw new Exception("Parking topilmadi.");
        }
    }

    public async Task<IEnumerable<Parking>> GetParkingsByLocationAsync(Country country)
    {
        if (country == null)
            throw new ArgumentNullException(nameof(country), "Country cannot be null.");
        return await _context.Set<Parking>()
            .Include(p => p.Address)
            .Where(p => p.Address.CountryId == country.BasaEntityId && !p.IsDeleted)
            .ToListAsync();
    }

    public async Task<bool> IsParkingAvailableAsync(Guid parkingId, DateTime startTime, DateTime endTime)
    {
        if (parkingId == Guid.Empty)
            throw new ArgumentException("Parking ID cannot be empty.", nameof(parkingId));

        var parking = await _context.Set<Parking>()
            .Include(p => p.Bookings)
            .FirstOrDefaultAsync(p => p.Id == parkingId && !p.IsDeleted);

        if (parking == null)
            throw new Exception("Parking topilmadi.");

        return !parking.Bookings!.Any(b =>
            (startTime < b.To && endTime > b.From)
        ) && parking.AvailableSpaces > 0;
    }
}   
