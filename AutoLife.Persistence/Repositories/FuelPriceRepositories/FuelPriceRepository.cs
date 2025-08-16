using AutoLife.Domain.Entities;
using AutoLife.Persistence.DataBaseContext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoLife.Persistence.Repositories.FuelPriceRepositories;

public class FuelPriceRepository(AppDbContext context) : GenericRepository<FuelPrice, AppDbContext>(context), IFuelPriceRepository
{
    public async Task<IEnumerable<FuelPrice>> GetPricesByFuelSubTypeIdAsync(Guid fuelSubTypeId)
    {
        if (fuelSubTypeId == Guid.Empty)
            throw new ArgumentException("Fuel SubType ID cannot be empty.", nameof(fuelSubTypeId));

        return await _context.Set<FuelPrice>()
            .Where(fp => fp.FuelSubTypeId == fuelSubTypeId && !fp.IsDeleted)
            .ToListAsync();
    }

}
