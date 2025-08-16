using AutoLife.Domain.Entities;
using AutoLife.Persistence.DataBaseContext;

namespace AutoLife.Persistence.Repositories.AddressRepositories;

public interface IAddressRepository : IGenericRepository<Address, AppDbContext>
{
    Task<IEnumerable<Address>> GetByLocationAsync(
      Guid? countryId = null,
      Guid? regionId = null,
      Guid? districtId = null,
      string? street = null);
}