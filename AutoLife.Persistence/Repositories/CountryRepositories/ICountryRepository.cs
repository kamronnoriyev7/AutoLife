using AutoLife.Domain.Entities;

namespace AutoLife.Persistence.Repositories.CountryRepositories;

public interface ICountryRepository : IGenericRepository<Country>
{
    Task<Country?> GetByNameAsync(string name, CancellationToken cancellationToken = default);
}