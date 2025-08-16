using AutoLife.Domain.Entities;
using AutoLife.Persistence.DataBaseContext;

namespace AutoLife.Persistence.Repositories.CountryRepositories;

public interface ICountryRepository : IGenericRepository<Country, AppDbContext>
{
    Task<Country?> GetByNameAsync(string name, CancellationToken cancellationToken = default);
}