using AutoLife.Application.DTOs.AddressDTOs;
using AutoLife.Domain.Entities;
using AutoLife.Persistence.DataBaseContext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoLife.Persistence.Repositories.AddressRepositories;

public class AddressRepository(AppDbContext context) : GenericRepository<Address, AppDbContext>(context), IAddressRepository
{
    public async Task<IEnumerable<Address>> GetByLocationAsync(Guid? countryId = null, Guid? regionId = null, Guid? districtId = null, string? street = null)
    {
        var query = _context.Set<Address>().AsQueryable();
        if (countryId.HasValue)
        {
            query = query.Where(a => a.CountryId == countryId.Value);
        }
        if (regionId.HasValue)
        {
            query = query.Where(a => a.RegionId == regionId.Value);
        }
        if (districtId.HasValue)
        {
            query = query.Where(a => a.DistrictId == districtId.Value);
        }
        if (!string.IsNullOrEmpty(street))
        {
            query = query.Where(a => a.Street.ToUpper().Contains(street.ToUpper()));
        }
        return await query.ToListAsync();
    }
}
