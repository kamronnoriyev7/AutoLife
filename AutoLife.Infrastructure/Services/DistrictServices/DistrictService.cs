using AutoLife.Application.DTOs.DistrictDTOs;
using AutoLife.Domain.Entities;
using AutoLife.Persistence.DataBaseContext;
using AutoLife.Persistence.Repositories;
using AutoLife.Persistence.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoLife.Infrastructure.Services.DistrictServices;

public class DistrictService : IDistrictService
{
    private readonly IUnitOfWork<AppDbContext> _unitOfWork;
    private readonly IGenericRepository<District, AppDbContext> _districtRepository;

    public DistrictService(IUnitOfWork<AppDbContext> unitOfWork, IGenericRepository<District, AppDbContext> districtRepository)
    {
        _unitOfWork = unitOfWork;
        _districtRepository = districtRepository;
    }

    public async Task<DistrictGetDto> CreateAsync(DistrictCreateDto dto, CancellationToken cancellationToken = default)
    {
        if (dto == null)
            throw new ArgumentNullException(nameof(dto), "District creation data cannot be null.");

        var district = new District
        {
            BasaEntityId = Guid.NewGuid(), 
            UzName = dto.UzName,
            RuName = dto.RuName,
            EnName = dto.EnName,
            RegionId = dto.RegionId,

        };

        await _districtRepository.AddAsync(district);
        await _unitOfWork.SaveChangesAsync();

        return new DistrictGetDto
        {
           Id = district.BasaEntityId,
           UzName = district.UzName,
           RuName  = district.RuName,
           EnName = district.EnName,
           RegionId = district.RegionId,
           RegionName = district.Region?.UzName ?? string.Empty 
        };
    }

    public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        if (id == Guid.Empty)
            throw new ArgumentException("Invalid district ID", nameof(id));

        var district = await _districtRepository.GetByIdAsync(id);
        if (district == null)
            throw new KeyNotFoundException($"District with ID {id} not found.");

        district.IsDeleted = true; 
        district.DeleteDate = DateTime.UtcNow;

        await _districtRepository.SoftDeleteAsync(district.BasaEntityId);
        await _unitOfWork.SaveChangesAsync();
        
        return true;
    }

    public async Task<List<DistrictGetDto>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var districts = await _districtRepository.FindAsync(d => !d.IsDeleted);
        return districts.Select(d => new DistrictGetDto
        {
            Id = d.BasaEntityId,
            UzName = d.UzName,
            RuName = d.RuName,
            EnName = d.EnName,
            RegionId = d.RegionId,
            RegionName = d.Region?.UzName ?? string.Empty
        }).ToList();
    }

    public async Task<IEnumerable<DistrictGetDto>> GetAllWithDetailsAsync(CancellationToken cancellationToken = default)
    {
        var districts = await _districtRepository.FindAsync(d => !d.IsDeleted);
        return districts.Select(d => new DistrictGetDto
        {
            Id = d.BasaEntityId,
            UzName = d.UzName,
            RuName = d.RuName,
            EnName = d.EnName,
            RegionId = d.RegionId,
            RegionName = d.Region?.UzName ?? string.Empty
        }).ToList();
    }

    public async Task<DistrictGetDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        if(id == Guid.Empty)
            throw new ArgumentException("Invalid district ID", nameof(id));

        var district = await _districtRepository.GetByIdAsync(id)
            ?? throw new KeyNotFoundException($"District with ID {id} not found.");

        return new DistrictGetDto
        {
            Id = district.BasaEntityId,
            UzName = district.UzName,
            RuName = district.RuName,
            EnName = district.EnName,
            RegionId = district.RegionId,
            RegionName = district.Region?.UzName ?? string.Empty
        };
    }

    public async Task<DistrictGetDto?> UpdateAsync(Guid id, DistrictCreateDto dto, CancellationToken cancellationToken = default)
    {
        if (id == Guid.Empty)
            throw new ArgumentException("Invalid district ID", nameof(id));

        if (dto == null)
            throw new ArgumentNullException(nameof(dto), "District update data cannot be null.");

        var district = await _districtRepository.GetByIdAsync(id);
        if (district == null)
            throw new KeyNotFoundException($"District with ID {id} not found.");

        district.UzName = dto.UzName;
        district.RuName = dto.RuName;
        district.EnName = dto.EnName;
        district.RegionId = dto.RegionId;

        _districtRepository.Update(district);
        await _unitOfWork.SaveChangesAsync();
        return new DistrictGetDto
        {
            Id = district.BasaEntityId,
            UzName = district.UzName,
            RuName = district.RuName,
            EnName = district.EnName,
            RegionId = district.RegionId,
            RegionName = district.Region?.UzName ?? string.Empty
        };
    }
}
