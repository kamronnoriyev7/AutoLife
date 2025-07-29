using AutoLife.Application.DTOs.DistrictDTOs;

namespace AutoLife.Infrastructure.Services.DistrictServices;

public interface IDistrictService
{
    Task<List<DistrictGetDto>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<DistrictGetDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<DistrictGetDto> CreateAsync(DistrictCreateDto dto, CancellationToken cancellationToken = default);
    Task<DistrictGetDto?> UpdateAsync(Guid id, DistrictCreateDto dto, CancellationToken cancellationToken = default);
    Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default);
    Task<IEnumerable<DistrictGetDto>> GetAllWithDetailsAsync(CancellationToken cancellationToken = default);
}