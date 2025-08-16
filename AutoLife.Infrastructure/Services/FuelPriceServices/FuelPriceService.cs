using AutoLife.Application.DTOs.FuelPriceDTOs;
using AutoLife.Domain.Entities;
using AutoLife.Persistence.DataBaseContext;
using AutoLife.Persistence.Repositories;
using AutoLife.Persistence.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoLife.Infrastructure.Services.FuelPriceServices;

public class FuelPriceService : IFuelPriceService
{
    private readonly IUnitOfWork<AppDbContext> _unitOfWork;
    private readonly IGenericRepository<FuelPrice, AppDbContext> _fuelPriceRepository;

    public FuelPriceService(IUnitOfWork<AppDbContext> unitOfWork, IGenericRepository<FuelPrice, AppDbContext> fuelPriceRepository)
    {
        _unitOfWork = unitOfWork;
        _fuelPriceRepository = fuelPriceRepository;
    }

    public async Task<FuelPrice> AddFuelPriceAsync(FuelPriceCreateDto fuelPriceDto)
    {
        if (fuelPriceDto == null)
            throw new ArgumentNullException(nameof(fuelPriceDto), "Fuel price cannot be null.");

        var fuelPrice = new FuelPrice
        {
            Id = Guid.NewGuid(),
            FuelSubTypeId = fuelPriceDto.FuelSubTypeId,
            Price = fuelPriceDto.Price,
            CreateDate = DateTime.UtcNow,
        };

        await _fuelPriceRepository.AddAsync(fuelPrice);
        await _unitOfWork.SaveChangesAsync();
        return fuelPrice;
    }

    public async Task DeleteFuelPriceAsync(Guid id)
    {
        if (id == Guid.Empty)
            throw new ArgumentException("Invalid fuel price ID", nameof(id));

        var fuelPrice = await _fuelPriceRepository.GetByIdAsync(id, asNoTracking: true);
        if (fuelPrice == null)
            throw new KeyNotFoundException($"Fuel price with ID {id} not found.");

        fuelPrice.IsDeleted = true; 
        fuelPrice.DeleteDate = DateTime.UtcNow;

        await _fuelPriceRepository.SoftDeleteAsync(fuelPrice.Id);
        await _unitOfWork.SaveChangesAsync();
    }

    public async Task<IEnumerable<FuelPrice>> GetAllFuelPricesAsync()
    {
        var fuelPrices = await _fuelPriceRepository.FindAsync(fp => !fp.IsDeleted);
        if (fuelPrices == null || !fuelPrices.Any())
            return Enumerable.Empty<FuelPrice>();

        return fuelPrices;
    }

    public async Task<FuelPrice> GetFuelPriceByIdAsync(Guid id)
    {
        if (id == Guid.Empty)
            throw new ArgumentException("Invalid fuel price ID", nameof(id));

        var fuelPrice = await _fuelPriceRepository.GetByIdAsync(id, asNoTracking: true);
        if (fuelPrice == null)
            throw new KeyNotFoundException($"Fuel price with ID {id} not found.");

        return fuelPrice;
    }

    public async Task<IEnumerable<FuelPrice>> GetFuelPricesByFuelSubTypeIdAsync(Guid fuelSubTypeId)
    {
        if (fuelSubTypeId == Guid.Empty)
            throw new ArgumentException("Invalid fuel subtype ID", nameof(fuelSubTypeId));

        var fuelPrices = await _fuelPriceRepository.FindAsync(fp => fp.FuelSubTypeId == fuelSubTypeId && !fp.IsDeleted);

        if (fuelPrices == null || !fuelPrices.Any())
            return Enumerable.Empty<FuelPrice>();

        return fuelPrices;
    }


    public async Task<FuelPrice> UpdateFuelPriceAsync(Guid id, FuelPriceCreateDto fuelPriceDto)
    {
        if (id == Guid.Empty)
            throw new ArgumentException("Invalid fuel price ID", nameof(id));

        if (fuelPriceDto == null)
            throw new ArgumentNullException(nameof(fuelPriceDto), "Fuel price cannot be null.");

        var existingFuelPrice = await _fuelPriceRepository.GetByIdAsync(id, asNoTracking: true);
        if (existingFuelPrice == null)
            throw new KeyNotFoundException($"Fuel price with ID {id} not found.");

        existingFuelPrice.FuelSubTypeId = fuelPriceDto.FuelSubTypeId;
        existingFuelPrice.Price = fuelPriceDto.Price;
        existingFuelPrice.UpdateDate = DateTime.UtcNow;

        _fuelPriceRepository.Update(existingFuelPrice);
        await _unitOfWork.SaveChangesAsync();
        
        return existingFuelPrice;
    }
}
