using AutoLife.Domain.Entities;

namespace AutoLife.Persistence.Repositories.AddressRepositories;

public interface IAddressRepository : IGenericRepository<Address>
{
    Task<IEnumerable<Address>> GetByLocationAsync(
      Guid? countryId = null,
      Guid? regionId = null,
      Guid? districtId = null,
      string? street = null);
}