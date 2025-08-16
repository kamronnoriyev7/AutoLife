using AutoLife.Domain.Entities;
using AutoLife.Persistence.DataBaseContext;

namespace AutoLife.Persistence.Repositories.NewsRepositories;

public interface INewsRepository : IGenericRepository<News, AppDbContext>
{
}