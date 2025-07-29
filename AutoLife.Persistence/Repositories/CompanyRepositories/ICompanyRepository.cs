using AutoLife.Domain.Entities;

namespace AutoLife.Persistence.Repositories.CompanyRepositories;

public interface ICompanyRepository : IGenericRepository<Company>
{
    Task<Company?> GetWithAllDetailsAsync(Guid id);
}