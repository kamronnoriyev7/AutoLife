using AutoLife.Domain.Entities;

namespace AutoLife.Persistence.Repositories.ServiceCenterRepositories;

public interface IServiceCenterRepository : IGenericRepository<ServiceCenter>
{
    Task<List<ServiceCenter>> GetAllWithDistrictAsync(CancellationToken cancellationToken = default);
}