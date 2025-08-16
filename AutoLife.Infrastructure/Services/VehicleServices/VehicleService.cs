using AutoLife.Application.DTOs.VehicleDTOs;
using AutoLife.Domain.Entities;
using AutoLife.Persistence.DataBaseContext;
using AutoLife.Persistence.Repositories;
using AutoLife.Persistence.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoLife.Infrastructure.Services.VehicleServices;

public class VehicleService : IVehicleService
{
    private readonly IUnitOfWork<AppDbContext> _unitOfWork;
    private readonly IGenericRepository<Vehicle, AppDbContext> _vehicleRepository;

    public VehicleService(IUnitOfWork<AppDbContext> unitOfWork, IGenericRepository<Vehicle, AppDbContext> vehicleRepository)
    {
        _unitOfWork = unitOfWork;
        _vehicleRepository = vehicleRepository;
    }

    public async Task AddVehicleAsync(VehicleCreateDto vehicleDto)
    {
        if (vehicleDto == null)
            throw new ArgumentNullException(nameof(vehicleDto), "Vehicle cannot be null.");
       
        var vehicle = new Vehicle
        {
            Id = Guid.NewGuid(),
            UserId = vehicleDto.UserId,
            Model = vehicleDto.Model,
            Brand = vehicleDto.Brand,
            FuelTypeId = vehicleDto.FuelTypeId,
            CreateDate = DateTime.UtcNow
        };
        await _vehicleRepository.AddAsync(vehicle);
        await _unitOfWork.SaveChangesAsync();
    }

    public async Task DeleteVehicleAsync(Guid id)
    {
        if (id == Guid.Empty)
            throw new ArgumentException("Invalid vehicle ID", nameof(id));
        var vehicle = await _vehicleRepository.GetByIdAsync(id, asNoTracking: true);
        if (vehicle == null)
            throw new KeyNotFoundException($"Vehicle with ID {id} not found.");
        vehicle.IsDeleted = true; // Soft delete
        vehicle.DeleteDate = DateTime.UtcNow;
        await _vehicleRepository.SoftDeleteAsync(vehicle.Id);
        await _unitOfWork.SaveChangesAsync();
    }

    public async Task<IEnumerable<VehicleResponseDto>> GetAllVehiclesAsync()
    {
        var vehicles = await _vehicleRepository.FindAsync(v => !v.IsDeleted);
        return vehicles.Select(v => new VehicleResponseDto
        {
            Id = v.Id,
            UserId = v.UserId,
            Model = v.Model,
            Brand = v.Brand,
            FuelTypeId = v.FuelTypeId,
            NumberPlate = v.NumberPlate,
        });
    }

    public async Task<VehicleResponseDto> GetVehicleByIdAsync(Guid id)
    {
        if (id == Guid.Empty)
            throw new ArgumentException("Invalid vehicle ID", nameof(id));
        var vehicle = await _vehicleRepository.GetByIdAsync(id, asNoTracking: true);
        if (vehicle == null)
            throw new KeyNotFoundException($"Vehicle with ID {id} not found.");
        return new VehicleResponseDto
        {
            Id = vehicle.Id,
            UserId = vehicle.UserId,
            Model = vehicle.Model,
            Brand = vehicle.Brand,
            FuelTypeId = vehicle.FuelTypeId,
            NumberPlate = vehicle.NumberPlate,
        };
    }

    public async Task<IEnumerable<VehicleResponseDto>> GetVehiclesByMakeAsync(string make)
    {
        if (string.IsNullOrWhiteSpace(make))
            throw new ArgumentException("Make cannot be null or empty.", nameof(make));
        var vehicles = await _vehicleRepository.FindAsync(v => v.Brand.Equals(make, StringComparison.OrdinalIgnoreCase) && !v.IsDeleted);
        return vehicles.Select(v => new VehicleResponseDto
        {
            Id = v.Id,
            UserId = v.UserId,
            Model = v.Model,
            Brand = v.Brand,
            FuelTypeId = v.FuelTypeId,
            NumberPlate = v.NumberPlate,
        });
    }

    public async Task<IEnumerable<VehicleResponseDto>> GetVehiclesByModelAsync(string model)
    {
        if (string.IsNullOrWhiteSpace(model))
            throw new ArgumentException("Model cannot be null or empty.", nameof(model));
        var vehicles = await _vehicleRepository.FindAsync(v => v.Model.Equals(model, StringComparison.OrdinalIgnoreCase) && !v.IsDeleted);
        return vehicles.Select(v => new VehicleResponseDto
        {
            Id = v.Id,
            UserId = v.UserId,
            Model = v.Model,
            Brand = v.Brand,
            FuelTypeId = v.FuelTypeId,
            NumberPlate = v.NumberPlate,
        });
    }

    public async Task<IEnumerable<VehicleResponseDto>> GetVehiclesByUserIdAsync(Guid userId)
    {
        if (userId == Guid.Empty)
            throw new ArgumentException("Invalid user ID", nameof(userId));
        var vehicles = await _vehicleRepository.FindAsync(v => v.UserId == userId && !v.IsDeleted);
        return vehicles.Select(v => new VehicleResponseDto
        {
            Id = v.Id,
            UserId = v.UserId,
            Model = v.Model,
            Brand = v.Brand,
            FuelTypeId = v.FuelTypeId,
            NumberPlate = v.NumberPlate,
        });
    }

    public async Task UpdateVehicleAsync(Guid id, VehicleCreateDto vehicleDto)
    {
        if (id == Guid.Empty)
            throw new ArgumentException("Invalid vehicle ID", nameof(id));
        if (vehicleDto == null)
            throw new ArgumentNullException(nameof(vehicleDto), "Vehicle cannot be null.");
        var vehicle = await _vehicleRepository.GetByIdAsync(id, asNoTracking: true);
        if (vehicle == null)
            throw new KeyNotFoundException($"Vehicle with ID {id} not found.");

        vehicle.Model = vehicleDto.Model;
        vehicle.Brand = vehicleDto.Brand;
        vehicle.FuelTypeId = vehicleDto.FuelTypeId;
        vehicle.NumberPlate = vehicleDto.NumberPlate;
        vehicle.UpdateDate = DateTime.UtcNow; 

        _vehicleRepository.Update(vehicle);
        await _unitOfWork.SaveChangesAsync();
    }
}
