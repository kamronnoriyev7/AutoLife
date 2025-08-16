using AutoLife.Domain.Entities;
using AutoLife.Persistence.DataBaseContext;

namespace AutoLife.Persistence.Repositories.ServiceCenterRepositories;

public interface IServiceCenterRepository : IGenericRepository<ServiceCenter, AppDbContext>
{
    Task<List<ServiceCenter>> GetAllWithDistrictAsync(CancellationToken cancellationToken = default);
}