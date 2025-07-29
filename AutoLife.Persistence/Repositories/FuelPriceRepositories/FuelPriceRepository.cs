using AutoLife.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoLife.Persistence.Repositories.FuelPriceRepositories;

public class FuelPriceRepository : GenericRepository<FuelPrice>, IFuelPriceRepository
{
    public FuelPriceRepository(DbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<FuelPrice>> GetPricesByFuelSubTypeIdAsync(Guid fuelSubTypeId)
    {
        if (fuelSubTypeId == Guid.Empty)
            throw new ArgumentException("Fuel SubType ID cannot be empty.", nameof(fuelSubTypeId));

        return await _context.Set<FuelPrice>()
            .Where(fp => fp.FuelSubTypeId == fuelSubTypeId && !fp.IsDeleted)
            .ToListAsync();
    }

    public async Task<IEnumerable<FuelPrice>> GetPricesByStationIdAsync(Guid stationId)
    {
        if (stationId == Guid.Empty)
            throw new ArgumentException("Station ID cannot be empty.", nameof(stationId));

        return await _context.Set<FuelPrice>()
            .Where(fp => fp.FuelStationId == stationId && !fp.IsDeleted)
            .ToListAsync();
    }
}
