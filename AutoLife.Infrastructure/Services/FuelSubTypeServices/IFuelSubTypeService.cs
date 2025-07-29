using AutoLife.Application.DTOs.FuelSubTypeDTOs;
using AutoLife.Domain.Entities;

namespace AutoLife.Infrastructure.Services.FuelSubTypeServices;

public interface IFuelSubTypeService
{
    Task<FuelSubType> CreateAsync(FuelSubTypeCreateDto dto, CancellationToken cancellationToken);
    Task<FuelSubType> UpdateAsync(Guid id, FuelSubTypeCreateDto dto, CancellationToken cancellationToken);
    Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken);
    Task<FuelSubType> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IEnumerable<FuelSubType>> GetAllAsync(CancellationToken cancellationToken);
}