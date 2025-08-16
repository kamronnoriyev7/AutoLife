using AutoLife.Domain.Entities;
using AutoLife.Persistence.DataBaseContext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoLife.Persistence.Repositories.FuelTypeRepositories;

public class FuelTypeRepository(AppDbContext context) : GenericRepository<FuelType, AppDbContext>(context), IFuelTypeRepository
{
    public async Task<List<FuelType>> GetAllWithSubTypesAsync(CancellationToken cancellationToken = default)
    {
        return await _context.Set<FuelType>()
            .Include(ft => ft.FuelSubTypes)
            .Where(ft => !ft.IsDeleted)
            .ToListAsync(cancellationToken);
    }

    public async Task<FuelType?> GetByIdWithSubTypesAsync(Guid id, CancellationToken cancellationToken = default)
    {
        if (id == Guid.Empty)
            throw new ArgumentException("Invalid fuel type ID", nameof(id));
        return await _context.Set<FuelType>()
            .Include(ft => ft.FuelSubTypes)
            .FirstOrDefaultAsync(ft => ft.Id == id && !ft.IsDeleted, cancellationToken);
    }

    public async Task<bool> IsFuelTypeNameUniqueAsync(string name, Guid? id = null, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Fuel type name cannot be null or empty.", nameof(name));
        var query = _context.Set<FuelType>().AsQueryable();
        if (id.HasValue)
        {
            query = query.Where(ft => ft.Id != id.Value);
        }
        return !await query.AnyAsync(ft => ft.Name.Equals(name, StringComparison.OrdinalIgnoreCase) && !ft.IsDeleted, cancellationToken);
    }
}

