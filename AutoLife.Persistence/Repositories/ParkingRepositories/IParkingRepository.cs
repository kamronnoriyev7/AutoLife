using AutoLife.Domain.Entities;

namespace AutoLife.Persistence.Repositories.ParkingRepositories;

public interface IParkingRepository : IGenericRepository<Parking>
{
    Task<IEnumerable<Parking>> GetParkingsByLocationAsync(Country country);
    Task<Parking> GetParkingByIdAsync(Guid id, bool includeDeleted = false);
    Task<IEnumerable<Parking>> GetAvailableParkingsAsync(DateTime startTime, DateTime endTime);
    Task<bool> IsParkingAvailableAsync(Guid parkingId, DateTime startTime, DateTime endTime);
}
