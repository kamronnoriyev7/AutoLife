using AutoLife.Application.DTOs.CompanyDTOs;
using AutoLife.Domain.Entities;
using AutoLife.Persistence.DataBaseContext;
using AutoLife.Persistence.Repositories;
using AutoLife.Persistence.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoLife.Infrastructure.Services.CompanyServices;

public class CompanyService : ICompanyService
{
    private readonly IUnitOfWork<AppDbContext> _unitOfWork;
    private readonly IGenericRepository<Company, AppDbContext> _companyRepository;

    public CompanyService(IUnitOfWork<AppDbContext> unitOfWork, IGenericRepository<Company, AppDbContext> companyRepository)
    {
        _unitOfWork = unitOfWork;
        _companyRepository = companyRepository;
    }

    public async Task<CompanyResponseDto> CreateAsync(CompanyCreateDto dto)
    {
        if (dto == null)
            throw new ArgumentNullException(nameof(dto), "CompanyCreateDto cannot be null");
        var company = new Company
        {
            Id = Guid.NewGuid(),
            Name = dto.Name,
            Description = dto.Description,
            Address = dto.Address,
            PhoneNumber = dto.PhoneNumber,
            Email = dto.Email,
            Website = dto.Website,
            LogoUrl = dto.LogoUrl,
            CreateDate = DateTime.UtcNow,

        };

        await _companyRepository.AddAsync(company);
        await _unitOfWork.SaveChangesAsync();

        return new CompanyResponseDto
        {
            Id = company.Id,
            Name = company.Name,
            Description = company.Description,
            Address = company.Address,
            PhoneNumber = company.PhoneNumber,
            Email = company.Email,
            UserId = company.UserId,
            Website = company.Website,
            LogoUrl = company.LogoUrl
        };
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var company = await _companyRepository.GetByIdAsync(id);
        if (company == null)
            return false;

        company.IsDeleted = true; 
        company.DeleteDate = DateTime.UtcNow;

        await _companyRepository.SoftDeleteAsync(company.Id);
        await _unitOfWork.SaveChangesAsync();
        return true;
    }

    public async Task<List<CompanyResponseDto>> GetAllAsync()
    {
        var companies = await _companyRepository.FindAsync(c => !c.IsDeleted);
        return companies.Select(c => new CompanyResponseDto
        {
            Id = c.Id,
            Name = c.Name,
            Description = c.Description,
            Address = c.Address,
            PhoneNumber = c.PhoneNumber,
            Email = c.Email,
            UserId = c.UserId,
            Website = c.Website,
            LogoUrl = c.LogoUrl
        }).ToList();
    }

    public async Task<IEnumerable<CompanyResponseDto>> GetAllWithDetailsAsync(CancellationToken cancellationToken = default)
    {
        var companies = await _companyRepository.FindAsync(c => !c.IsDeleted);

        if (companies == null || !companies.Any())
            return Enumerable.Empty<CompanyResponseDto>();

        return companies.Select(c => new CompanyResponseDto
        {
            Id = c.Id,
            Name = c.Name,
            Description = c.Description,
            Address = c.Address,
            PhoneNumber = c.PhoneNumber,
            Email = c.Email,
            UserId = c.UserId,
            Website = c.Website,
            LogoUrl = c.LogoUrl
        });
    }

    public async Task<CompanyResponseDto?> GetByIdAsync(Guid id)
    {
        if (id == Guid.Empty)
            throw new ArgumentException("Invalid company ID", nameof(id));

        var company = await _companyRepository.GetByIdAsync(id)
            ?? throw new KeyNotFoundException($"Company with ID {id} not found.");

        return new CompanyResponseDto
        {
            Id = company.Id,
            Name = company.Name, 
            Description = company.Description,
            Address = company.Address,
            PhoneNumber = company.PhoneNumber,
            Email = company.Email,
            UserId = company.UserId,
            Website = company.Website,
            LogoUrl = company.LogoUrl
        };
    }

    public async Task<CompanyResponseDto?> UpdateAsync(Guid id, CompanyUpdateDto dto)
    {
        if (id == Guid.Empty)
            throw new ArgumentException("Invalid company ID", nameof(id));
        if (dto == null)
            throw new ArgumentNullException(nameof(dto), "CompanyUpdateDto cannot be null");
        var company = await _companyRepository.GetByIdAsync(id);
        if (company == null)
            return null;
        company.Name = dto.Name;
        company.Description = dto.Description;
        company.Address = dto.Address;
        company.PhoneNumber = dto.PhoneNumber;
        company.Email = dto.Email;
        company.Website = dto.Website;
        company.LogoUrl = dto.LogoUrl;
        company.UpdateDate = DateTime.UtcNow;

        _companyRepository.Update(company);
        await _unitOfWork.SaveChangesAsync();

        return new CompanyResponseDto
        {
            Id = company.Id,
            Name = company.Name,
            Description = company.Description,
            Address = company.Address,
            PhoneNumber = company.PhoneNumber,
            Email = company.Email,
            UserId = company.UserId,
            Website = company.Website,
            LogoUrl = company.LogoUrl
        };
    }
}
