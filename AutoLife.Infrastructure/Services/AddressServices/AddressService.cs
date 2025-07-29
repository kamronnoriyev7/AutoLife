using AutoLife.Application.DTOs.AddressDTOs;
using AutoLife.Domain.Entities;
using AutoLife.Persistence.DataBaseContext;
using AutoLife.Persistence.Repositories;
using AutoLife.Persistence.Repositories.AddressRepositories;
using AutoLife.Persistence.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoLife.Infrastructure.Services.AddressServices;

public class AddressService : IAddressService
{
    private readonly UnitOfWork<AppDbContext> _unitOfWork;
    private readonly IGenericRepository<Address> _addressRepository;

    public AddressService(UnitOfWork<AppDbContext> unitOfWork, IGenericRepository<Address> addressRepository)
    {
        _unitOfWork = unitOfWork;
        _addressRepository = addressRepository;
    }

    public async Task<bool> AddressExistsAsync(Guid id)
    {
        if (id == Guid.Empty)
            throw new ArgumentException("Address ID cannot be empty", nameof(id));

        var address = await _addressRepository.GetByIdAsync(id);
        return address != null && !address.IsDeleted;
    }

    public async Task<AddressResponseDto> CreateAddressAsync(CreateAddressDto dto)
    {
        var address = new Address
        {
            Id = Guid.NewGuid(),
            UserId = dto.UserId,
            CountryId = dto.CountryId,
            RegionId = dto.RegionId,
            DistrictId = dto.DistrictId,
            Street = dto.Street,
            CreateDate = DateTime.UtcNow
        };

        await _addressRepository.AddAsync(address);
        await _unitOfWork.SaveChangesAsync();

        return new AddressResponseDto
        {
            Id = address.Id,
            UserId = address.UserId,
            CountryId = address.CountryId,
            RegionId = address.RegionId,
            DistrictId = address.DistrictId,
            Street = address.Street
        };
    }

    public async Task<bool> DeleteAddressAsync(Guid id)
    {
        var address = await _addressRepository.GetByIdAsync(id);
        if (address is null || address.IsDeleted)
            return false;

        address.IsDeleted = true;
        _addressRepository.Update(address);
        await _unitOfWork.SaveChangesAsync();

        return true;
    }

    public async Task<AddressResponseDto?> GetAddressByIdAsync(Guid id)
    {
        var address = await _addressRepository.GetByIdAsync(id);
        if (address == null || address.IsDeleted)
            return null;

        return new AddressResponseDto
        {
            Id = address.Id,
            UserId = address.UserId,
            CountryId = address.CountryId,
            RegionId = address.RegionId,
            DistrictId = address.DistrictId,
            Street = address.Street
        };
    }

    public async Task<IEnumerable<AddressResponseDto>> GetAddressesByUserIdAsync(Guid userId)
    {
         var addresses = await _addressRepository.FindAsync(a => !a.IsDeleted);

        return addresses.Select(address => new AddressResponseDto
        {
            Id = address.Id,
            UserId = address.UserId,
            CountryId = address.CountryId,
            RegionId = address.RegionId,
            DistrictId = address.DistrictId,
            Street = address.Street
        });
    }

    public async Task<IEnumerable<AddressResponseDto>> GetAllAddressesAsync()
    {
        var addresses = await _addressRepository.FindAsync(a => !a.IsDeleted);

        return addresses.Select(address => new AddressResponseDto
        {
            Id = address.Id,
            UserId = address.UserId,
            CountryId = address.CountryId,
            RegionId = address.RegionId,
            DistrictId = address.DistrictId,
            Street = address.Street
        });
    }

    public async Task<AddressResponseDto> UpdateAddressAsync(UpdateAddressDto dto)
    {
        var address = await _addressRepository.GetByIdAsync(dto.Id);
        if (address is null || address.IsDeleted)
            throw new Exception("Address not found!");

        address.CountryId = dto.CountryId;
        address.RegionId = dto.RegionId;
        address.DistrictId = dto.DistrictId;
        address.Street = dto.Street;
        address.UpdateDate = DateTime.UtcNow;

        _addressRepository.Update(address);
        await _unitOfWork.SaveChangesAsync();

        return new AddressResponseDto
        {
            Id = address.Id,
            UserId = address.UserId,
            CountryId = address.CountryId,
            RegionId = address.RegionId,
            DistrictId = address.DistrictId,
            Street = address.Street
        };
    }
}

