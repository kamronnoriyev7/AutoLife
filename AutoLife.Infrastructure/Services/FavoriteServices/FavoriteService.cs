using AutoLife.Application.DTOs.FavoriteDTOs;
using AutoLife.Domain.Entities;
using AutoLife.Persistence.DataBaseContext;
using AutoLife.Persistence.Repositories;
using AutoLife.Persistence.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoLife.Infrastructure.Services.FavoriteServices;

public class FavoriteService : IFavoriteService
{
    private readonly IGenericRepository<Favorite, AppDbContext>  _favoriteRepository;
    private readonly IUnitOfWork<AppDbContext> _unitOfWork;

    public FavoriteService(IGenericRepository<Favorite, AppDbContext> favoriteRepository, IUnitOfWork<AppDbContext> unitOfWork)
    {
        _favoriteRepository = favoriteRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task AddFavoriteAsync(FavoriteCreateDto favorite)
    {
        if (favorite == null)
            throw new ArgumentNullException(nameof(favorite), "Favorite cannot be null.");

        var existingFavorite = await _favoriteRepository.GetByIdAsync(favorite.UserId, asNoTracking: true);
        if (existingFavorite != null)
            throw new InvalidOperationException($"Favorite for user {favorite.UserId} already exists.");

        var newFavorite = new Favorite
        {
            Id = Guid.NewGuid(),
            UserId = favorite.UserId,
            FuelStationId = favorite.FuelStationId,
            ParkingId = favorite.ParkingId,
            VehicleId = favorite.VehicleId,
            ServiceCenterId = favorite.ServiceCenterId,
            CreateDate = DateTime.UtcNow,
        };
        await _favoriteRepository.AddAsync(newFavorite);
        await _unitOfWork.SaveChangesAsync();
    }

    public async Task<ICollection<Favorite>> GetAllFavoritesAsync()
    {
        var favorites = await _favoriteRepository.GetAllAsync();
        if (favorites == null || !favorites.Any())
            throw new KeyNotFoundException("No favorites found.");

        return favorites.ToList();
    }

    public async Task<Favorite> GetFavoriteByIdAsync(Guid id)
    {
        if (id == Guid.Empty)
            throw new ArgumentException("Invalid favorite ID", nameof(id));

        var favorite = await _favoriteRepository.GetByIdAsync(id, asNoTracking: true);
        if (favorite == null)
            throw new KeyNotFoundException($"Favorite with ID {id} not found.");

        return favorite;
    }

    public Task RemoveFavoriteAsync(Guid id)
    {
        throw new NotImplementedException();
    }
}
