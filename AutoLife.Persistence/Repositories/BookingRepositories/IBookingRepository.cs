using AutoLife.Domain.Entities;
using AutoLife.Domain.Enums;

namespace AutoLife.Persistence.Repositories.BookingRepositories;

public interface IBookingRepository : IGenericRepository<Booking>
{
    Task<IEnumerable<Booking>> GetAllByUserIdAsync(Guid userId, CancellationToken cancellationToken = default);
    Task<IEnumerable<Booking>> GetAllByStatusAsync(BookingStatus status, CancellationToken cancellationToken = default);
    Task<IEnumerable<Booking>> GetAllWithDetailsAsync(CancellationToken cancellationToken = default);
    Task<Booking?> GetByIdWithDetailsAsync(Guid id, CancellationToken cancellationToken = default);
}