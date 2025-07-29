using AutoLife.Application.DTOs.FavoriteDTOs;
using AutoLife.Domain.Entities;

namespace AutoLife.Infrastructure.Services.FavoriteServices;

public interface IFavoriteService
{
    Task AddFavoriteAsync(FavoriteCreateDto favorite);
    Task RemoveFavoriteAsync(Guid id);
    Task<ICollection<Favorite>> GetAllFavoritesAsync();
    Task<Favorite> GetFavoriteByIdAsync(Guid id);
}