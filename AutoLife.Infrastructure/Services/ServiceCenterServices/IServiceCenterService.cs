using AutoLife.Application.DTOs.ServiceCentersDTOs;
using AutoLife.Domain.Entities;

namespace AutoLife.Infrastructure.Services.ServiceCenterServices;

public interface IServiceCenterService
{
    Task AddServiceCenterAsync(CreateServiceCenterDto serviceCenterDto);
    Task UpdateServiceCenterAsync(Guid id, CreateServiceCenterDto serviceCenterDto);
    Task DeleteServiceCenterAsync(Guid id);
    Task<ServiceCenterResponseDto> GetServiceCenterByIdAsync(Guid id);
    Task<IEnumerable<ServiceCenterResponseDto>> GetAllServiceCentersAsync();
    Task<IEnumerable<ServiceCenterResponseDto>> GetServiceCentersByLocationAsync(string location);
    Task<IEnumerable<ServiceCenterResponseDto>> GetServiceCentersByTypeAsync(string type);
    Task<ICollection<ServiceCenterResponseDto>> GetServiceCentersByNearbyLocationAsync(
        double latitude,
        double longitude,
        double radiusKm,
        CancellationToken cancellationToken = default);
}