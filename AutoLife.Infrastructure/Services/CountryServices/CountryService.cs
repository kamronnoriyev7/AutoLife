using AutoLife.Application.DTOs.CountryDTOs;
using AutoLife.Domain.Entities;
using AutoLife.Persistence.DataBaseContext;
using AutoLife.Persistence.Repositories;
using AutoLife.Persistence.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoLife.Infrastructure.Services.CountryServices;

public class CountryService : ICountryService
{
    private readonly IUnitOfWork<AppDbContext> _unitOfWork;
    private readonly IGenericRepository<Country> _countryRepository;

    public CountryService(IUnitOfWork<AppDbContext> unitOfWork, IGenericRepository<Country> countryRepository)
    {
        _unitOfWork = unitOfWork;
        _countryRepository = countryRepository;
    }

    public async Task<CountryDto> CreateAsync(CreateCountryDto dto)
    {
        if (dto == null) throw new ArgumentNullException(nameof(dto), "CreateCountryDto cannot be null");

        var country = new Country
        {
            UzName = dto.UzName,
            RuName = dto.RuName,
            EnName = dto.EnName,
            BasaEntityId = Guid.NewGuid()
        };
        await _countryRepository.AddAsync(country);
        await _unitOfWork.SaveChangesAsync();

        return new CountryDto
        {
            Id = country.BasaEntityId,
            UzName = country.UzName,
            RuName = country.RuName,
            EnName = country.EnName
        };
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        if (id == Guid.Empty)
            throw new ArgumentException("Invalid country ID", nameof(id));

        var country = await _countryRepository.GetByIdAsync(id)
            ?? throw new KeyNotFoundException("Country not found with the provided ID.");

        country.IsDeleted = true; 
        country.DeleteDate = DateTime.UtcNow;

        await _countryRepository.SoftDeleteAsync(country.BasaEntityId);
        await _unitOfWork.SaveChangesAsync();
        return true;
    }

    public async Task<List<CountryDto>> GetAllAsync()
    {
        var countries = await _countryRepository.FindAsync(c => !c.IsDeleted)
            ?? throw new Exception("No countries found.");

        return countries.Select(c => new CountryDto
        {
            Id = c.BasaEntityId,
            UzName = c.UzName,
            RuName = c.RuName,
            EnName = c.EnName
        }).ToList();
    }

    public async Task<IEnumerable<CountryDto>> GetAllWithDetailsAsync(CancellationToken cancellationToken = default)
    {
        var countries = await _countryRepository.FindAsync(c => !c.IsDeleted)
            ?? throw new Exception("No countries found.");

        return countries.Select(c => new CountryDto
        {
            Id = c.BasaEntityId,
            UzName = c.UzName,
            RuName = c.RuName,
            EnName = c.EnName
        });
    }

    public async Task<CountryDto?> GetByIdAsync(Guid id)
    {
        if (id == Guid.Empty)
            throw new ArgumentException("Invalid country ID", nameof(id));

        var country = await _countryRepository.GetByIdAsync(id);
        if (country == null)
            return null;

        return new CountryDto
        {
            Id = country.BasaEntityId,
            UzName = country.UzName,
            RuName = country.RuName,
            EnName = country.EnName,
        };
    }

    public async Task<CountryDto?> UpdateAsync(UpdateCountryDto dto)
    {
        if (dto == null) throw new ArgumentNullException(nameof(dto), "UpdateCountryDto cannot be null");
        var country = await _countryRepository.GetByIdAsync(dto.Id)
            ?? throw new KeyNotFoundException("Country not found with the provided ID.");

        country.UzName = dto.UzName;
        country.RuName = dto.RuName;
        country.EnName = dto.EnName;

        _countryRepository.Update(country);
        await _unitOfWork.SaveChangesAsync();

        return new CountryDto
        {
            Id = country.BasaEntityId,
            UzName = country.UzName,
            RuName = country.RuName,
            EnName = country.EnName
        };
    }
}
