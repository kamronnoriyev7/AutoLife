using AutoLife.Application.DTOs.CountryDTOs;

namespace AutoLife.Infrastructure.Services.CountryServices;

public interface ICountryService
{
    Task<List<CountryDto>> GetAllAsync();
    Task<CountryDto?> GetByIdAsync(Guid id);
    Task<CountryDto> CreateAsync(CreateCountryDto dto);
    Task<CountryDto?> UpdateAsync(UpdateCountryDto dto);
    Task<bool> DeleteAsync(Guid id);
    Task<IEnumerable<CountryDto>> GetAllWithDetailsAsync(CancellationToken cancellationToken = default);
}