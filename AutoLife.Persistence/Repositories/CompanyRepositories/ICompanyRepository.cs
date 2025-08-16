using AutoLife.Domain.Entities;
using AutoLife.Persistence.DataBaseContext;

namespace AutoLife.Persistence.Repositories.CompanyRepositories;

public interface ICompanyRepository : IGenericRepository<Company, AppDbContext>
{
    Task<Company?> GetWithAllDetailsAsync(Guid id);
}