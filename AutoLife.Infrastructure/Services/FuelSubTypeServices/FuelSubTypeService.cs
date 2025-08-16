using AutoLife.Application.DTOs.FuelSubTypeDTOs;
using AutoLife.Domain.Entities;
using AutoLife.Persistence.DataBaseContext;
using AutoLife.Persistence.Repositories;
using AutoLife.Persistence.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoLife.Infrastructure.Services.FuelSubTypeServices;

public class FuelSubTypeService : IFuelSubTypeService
{
    private readonly IUnitOfWork<AppDbContext> _unitOfWork;
    private readonly IGenericRepository<FuelSubType, AppDbContext> _fuelSubTypeRepository;

    public FuelSubTypeService(IUnitOfWork<AppDbContext> unitOfWork, IGenericRepository<FuelSubType, AppDbContext> fuelSubTypeRepository)
    {
        _unitOfWork = unitOfWork;
        _fuelSubTypeRepository = fuelSubTypeRepository;
    }

    public async Task<FuelSubType> CreateAsync(FuelSubTypeCreateDto dto, CancellationToken cancellationToken)
    {
        if (dto == null)
            throw new ArgumentNullException(nameof(dto), "Fuel sub-type creation data cannot be null.");

        var fuelSubType = new FuelSubType
        {
            Id = Guid.NewGuid(),
            Name = dto.Name,
            Description = dto.Description,
            CreateDate = DateTime.UtcNow
        };

        await _fuelSubTypeRepository.AddAsync(fuelSubType);
        await _unitOfWork.SaveChangesAsync();
        return fuelSubType;
    }

    public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        if (id == Guid.Empty)
            throw new ArgumentException("Invalid fuel sub-type ID", nameof(id));

        var fuelSubType = await _fuelSubTypeRepository.GetByIdAsync(id, asNoTracking: true);
        if (fuelSubType == null)
            throw new KeyNotFoundException($"Fuel sub-type with ID {id} not found.");

        fuelSubType.IsDeleted = true; 
        fuelSubType.DeleteDate = DateTime.UtcNow;

        await _fuelSubTypeRepository.SoftDeleteAsync(fuelSubType.Id);
        await _unitOfWork.SaveChangesAsync();
        return true;
    }

    public async Task<IEnumerable<FuelSubType>> GetAllAsync(CancellationToken cancellationToken)
    {
        var fuelSubTypes = await _fuelSubTypeRepository.FindAsync(f => !f.IsDeleted);
        return fuelSubTypes.ToList();
    }

    public async Task<FuelSubType> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        if (id == Guid.Empty)
            throw new ArgumentException("Invalid fuel sub-type ID", nameof(id));

        var fuelSubType = await _fuelSubTypeRepository.GetByIdAsync(id, asNoTracking: true);
        if (fuelSubType == null)
            throw new KeyNotFoundException($"Fuel sub-type with ID {id} not found.");

        return fuelSubType;
    }

    public async Task<FuelSubType> UpdateAsync( Guid id, FuelSubTypeCreateDto dto, CancellationToken cancellationToken)
    {
        if (dto == null)
            throw new ArgumentNullException(nameof(dto), "Fuel sub-type update data cannot be null.");

        var fuelSubType = await _fuelSubTypeRepository.GetByIdAsync(id);
        if (fuelSubType == null)
            throw new KeyNotFoundException($"Fuel sub-type with ID {id} not found.");

        fuelSubType.Name = dto.Name;
        fuelSubType.Description = dto.Description;
        fuelSubType.UpdateDate = DateTime.UtcNow;

        _fuelSubTypeRepository.Update(fuelSubType);
        await _unitOfWork.SaveChangesAsync();
        return fuelSubType;
    }
}
