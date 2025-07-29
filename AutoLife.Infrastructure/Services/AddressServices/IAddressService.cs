using AutoLife.Application.DTOs.AddressDTOs;

namespace AutoLife.Infrastructure.Services.AddressServices;

public interface IAddressService
{
    Task<IEnumerable<AddressResponseDto>> GetAllAddressesAsync();
    Task<AddressResponseDto?> GetAddressByIdAsync(Guid id);
    Task<AddressResponseDto> CreateAddressAsync(CreateAddressDto dto);
    Task<AddressResponseDto> UpdateAddressAsync(UpdateAddressDto dto);
    Task<bool> DeleteAddressAsync(Guid id);
    Task<bool> AddressExistsAsync(Guid id);
    Task<IEnumerable<AddressResponseDto>> GetAddressesByUserIdAsync(Guid userId);
}