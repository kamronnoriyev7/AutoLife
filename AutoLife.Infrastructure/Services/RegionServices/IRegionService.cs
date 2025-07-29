using AutoLife.Application.DTOs.RegionDTOs;

namespace AutoLife.Infrastructure.Services.RegionServices;

public interface IRegionService
{
    Task<RegionResponseDto> AddRegionAsync(CreateRegionDto regionDto);
    Task<RegionResponseDto> UpdateRegionAsync(Guid id, CreateRegionDto regionDto);
    Task DeleteRegionAsync(Guid id);
    Task<RegionResponseDto> GetRegionByIdAsync(Guid id);
    Task<IEnumerable<RegionResponseDto>> GetAllRegionsAsync();
    Task<IEnumerable<RegionResponseDto>> GetRegionsByCountryIdAsync(Guid countryId);
}