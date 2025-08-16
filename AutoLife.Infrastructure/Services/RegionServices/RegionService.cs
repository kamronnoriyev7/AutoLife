using AutoLife.Application.DTOs.RegionDTOs;
using AutoLife.Domain.Entities;
using AutoLife.Persistence.DataBaseContext;
using AutoLife.Persistence.Repositories;
using AutoLife.Persistence.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoLife.Infrastructure.Services.RegionServices;

public class RegionService : IRegionService
{
    private readonly IUnitOfWork<AppDbContext> _unitOfWork;
    private readonly IGenericRepository<Region, AppDbContext> _regionRepository;

    public RegionService(IUnitOfWork<AppDbContext> unitOfWork, IGenericRepository<Region, AppDbContext> regionRepository)
    {
        _unitOfWork = unitOfWork;
        _regionRepository = regionRepository;
    }

    public async Task<RegionResponseDto> AddRegionAsync(CreateRegionDto regionDto)
    {
        if (regionDto == null)
            throw new ArgumentNullException(nameof(regionDto), "Region cannot be null.");

        var newRegion = new Region
        {
            BasaEntityId = Guid.NewGuid(),
            UzName = regionDto.UzName,
            RuName = regionDto.RuName,
            EnName = regionDto.EnName,
            CountryId = regionDto.CountryId,
            CreateDate = DateTime.UtcNow,
        };
        await _regionRepository.AddAsync(newRegion);
        await _unitOfWork.SaveChangesAsync();

        return new RegionResponseDto
        {
            Id = newRegion.BasaEntityId,
            CountryId = newRegion.CountryId,
            UzName = newRegion.UzName,
            RuName = newRegion.RuName,
            EnName = newRegion.EnName
        };
    }


    public async Task DeleteRegionAsync(Guid id)
    {
        if (id == Guid.Empty)
            throw new ArgumentException("Invalid region ID", nameof(id));
        var region = await _regionRepository.GetByIdAsync(id, asNoTracking: true);
        if (region == null)
            throw new KeyNotFoundException($"Region with ID {id} not found.");

        region.IsDeleted = true; // Soft delete
        region.DeleteDate = DateTime.UtcNow;
        await _regionRepository.SoftDeleteAsync(region.BasaEntityId);
        await _unitOfWork.SaveChangesAsync();
    }

    public async Task<IEnumerable<RegionResponseDto>> GetAllRegionsAsync()
    {
        var regions = await _regionRepository.FindAsync(r => !r.IsDeleted);
        return regions.Select(r => new RegionResponseDto
        {
            Id = r.BasaEntityId,
            CountryId = r.CountryId,
            UzName = r.UzName,
            RuName = r.RuName,
            EnName = r.EnName
        }).ToList();
    }

    public async Task<RegionResponseDto> GetRegionByIdAsync(Guid id)
    {
        if (id == Guid.Empty)
            throw new ArgumentException("Invalid region ID", nameof(id));
        var region = await _regionRepository.GetByIdAsync(id, asNoTracking: true);
        if (region == null)
            throw new KeyNotFoundException($"Region with ID {id} not found.");
        return new RegionResponseDto
        {
            Id = region.BasaEntityId,
            CountryId = region.CountryId,
            UzName = region.UzName,
            RuName = region.RuName,
            EnName = region.EnName
        };
    }

    public async Task<IEnumerable<RegionResponseDto>> GetRegionsByCountryIdAsync(Guid countryId)
    {
        if (countryId == Guid.Empty)
            throw new ArgumentException("Invalid country ID", nameof(countryId));
        var regions = await _regionRepository.FindAsync(r => r.CountryId == countryId && !r.IsDeleted);
        return regions.Select(r => new RegionResponseDto
        {
            Id = r.BasaEntityId,
            CountryId = r.CountryId,
            UzName = r.UzName,
            RuName = r.RuName,
            EnName = r.EnName
        }).ToList();
    }

    public async Task<RegionResponseDto> UpdateRegionAsync(Guid id, CreateRegionDto regionDto)
    {
        if (id == Guid.Empty)
            throw new ArgumentException("Invalid region ID", nameof(id));
        if (regionDto == null)
            throw new ArgumentNullException(nameof(regionDto), "Region cannot be null.");
        var region = await _regionRepository.GetByIdAsync(id, asNoTracking: true);
        if (region == null)
            throw new KeyNotFoundException($"Region with ID {id} not found.");
        region.UzName = regionDto.UzName;
        region.RuName = regionDto.RuName;
        region.EnName = regionDto.EnName;
        region.CountryId = regionDto.CountryId;
        _regionRepository.Update(region);
        await _unitOfWork.SaveChangesAsync();
        return new RegionResponseDto
        {
            Id = region.BasaEntityId,
            CountryId = region.CountryId,
            UzName = region.UzName,
            RuName = region.RuName,
            EnName = region.EnName
        };
    }
}
