using AutoLife.Domain.Entities;
using AutoLife.Persistence.DataBaseContext;
using AutoLife.Persistence.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace AutoLife.Persistence.Repositories.CountryRepositories;

public class CountryRepository : GenericRepository<Country>, ICountryRepository
{
    public CountryRepository(DbContext context) : base(context)
    {
    }

    public async Task<Country?> GetByNameAsync(string name, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Country name cannot be null or empty.", nameof(name));

        return await _context.Set<Country>()
            .AsNoTracking()
            .FirstOrDefaultAsync(c => c.UzName.Equals(name, StringComparison.OrdinalIgnoreCase), cancellationToken);
    }
}
