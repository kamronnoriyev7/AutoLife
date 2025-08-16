using AutoLife.Domain.Entities;
using AutoLife.Persistence.DataBaseContext;

namespace AutoLife.Persistence.Repositories.FavoriteRepositories;

public interface IFavoriteRepository : IGenericRepository<Favorite, AppDbContext>
{
}