using AutoLife.Application.DTOs.FuelTypeDTOs;
using AutoLife.Domain.Entities;
using AutoLife.Persistence.DataBaseContext;
using AutoLife.Persistence.Repositories;
using AutoLife.Persistence.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoLife.Infrastructure.Services.FuelTypeServices;

public class FuelTypeService : IFuelTypeService
{
    private readonly IUnitOfWork<AppDbContext> _unitOfWork;
    private readonly IGenericRepository<FuelType, AppDbContext> _fuelTypeRepository;

    public FuelTypeService(IUnitOfWork<AppDbContext> unitOfWork, IGenericRepository<FuelType, AppDbContext> fuelTypeRepository)
    {
        _unitOfWork = unitOfWork;
        _fuelTypeRepository = fuelTypeRepository;
    }

    public async Task<FuelType> AddFuelTypeAsync(FuelTypeCreateDto fuelTypeDto)
    {
        if (fuelTypeDto == null)
            throw new ArgumentNullException(nameof(fuelTypeDto), "Fuel type cannot be null.");
       
        var fuelType = new FuelType
        {
            Id = Guid.NewGuid(),
            Name = fuelTypeDto.Name,
            Description = fuelTypeDto.Description,
            CreateDate = DateTime.UtcNow,
        };
        await _fuelTypeRepository.AddAsync(fuelType);
        await _unitOfWork.SaveChangesAsync();
        return fuelType;
    }

    public async Task DeleteFuelTypeAsync(Guid id)
    {
        if (id == Guid.Empty)
            throw new ArgumentException("Invalid fuel type ID", nameof(id));

        var fuelType = await _fuelTypeRepository.GetByIdAsync(id, asNoTracking: true);
        if (fuelType == null)
            throw new KeyNotFoundException($"Fuel type with ID {id} not found.");

        fuelType.IsDeleted = true; // Soft delete
        fuelType.DeleteDate = DateTime.UtcNow;

        await _fuelTypeRepository.SoftDeleteAsync(fuelType.Id);
        await _unitOfWork.SaveChangesAsync();
    }

    public async Task<IEnumerable<FuelType>> GetAllFuelTypesAsync()
    {
        var fuelTypes = await _fuelTypeRepository.FindAsync(f => !f.IsDeleted);
        return fuelTypes.ToList();
    }

    public async Task<FuelType> GetFuelTypeByIdAsync(Guid id)
    {
        if (id == Guid.Empty)
            throw new ArgumentException("Invalid fuel type ID", nameof(id));

        var fuelType = await _fuelTypeRepository.GetByIdAsync(id, asNoTracking: true);
        if (fuelType == null)
            throw new KeyNotFoundException($"Fuel type with ID {id} not found.");

        return fuelType;
    }

    public async Task<IEnumerable<FuelType>> GetFuelTypesByStationIdAsync(Guid stationId)
    {
        if (stationId == Guid.Empty)
            throw new ArgumentException("Invalid station ID", nameof(stationId));
        var fuelTypes = await _fuelTypeRepository.FindAsync(f => f.FuelStationId == stationId && !f.IsDeleted);
        if (fuelTypes == null || !fuelTypes.Any())
            return Enumerable.Empty<FuelType>();
        return fuelTypes.ToList();
    }

    public async Task<FuelType> UpdateFuelTypeAsync(Guid id, FuelTypeCreateDto fuelTypeDto)
    {
        if (id == Guid.Empty)
            throw new ArgumentException("Invalid fuel type ID", nameof(id));

        if (fuelTypeDto == null)
            throw new ArgumentNullException(nameof(fuelTypeDto), "Fuel type cannot be null.");

        var fuelType = await _fuelTypeRepository.GetByIdAsync(id, asNoTracking: true);
        if (fuelType == null)
            throw new KeyNotFoundException($"Fuel type with ID {id} not found.");

        fuelType.Name = fuelTypeDto.Name;
        fuelType.Description = fuelTypeDto.Description;
        fuelType.UpdateDate = DateTime.UtcNow;

        _fuelTypeRepository.Update(fuelType);
        await _unitOfWork.SaveChangesAsync();
        return fuelType;
    }
}
