using AutoLife.Application.DTOs.CompanyDTOs;

namespace AutoLife.Infrastructure.Services.CompanyServices;

public interface ICompanyService
{
    Task<List<CompanyResponseDto>> GetAllAsync();
    Task<CompanyResponseDto?> GetByIdAsync(Guid id);
    Task<CompanyResponseDto> CreateAsync(CompanyCreateDto dto);
    Task<CompanyResponseDto?> UpdateAsync(Guid id, CompanyUpdateDto dto);
    Task<bool> DeleteAsync(Guid id);
    Task<IEnumerable<CompanyResponseDto>> GetAllWithDetailsAsync(CancellationToken cancellationToken = default);
}