using AutoLife.Domain.Entities;
using AutoLife.Domain.Enums;
using AutoLife.Persistence.DataBaseContext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoLife.Persistence.Repositories.BookingRepositories;

public class BookingRepository : GenericRepository<Booking, AppDbContext>, IBookingRepository
{

    public BookingRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Booking>> GetAllByStatusAsync(BookingStatus status, CancellationToken cancellationToken = default)
    {
        return await _context.Set<Booking>()
            .Where(b => b.Status == status)
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<Booking>> GetAllByUserIdAsync(Guid userId, CancellationToken cancellationToken = default)
    {
        return await _context.Set<Booking>()
            .Where(b => b.UserId == userId)
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<Booking>> GetAllWithDetailsAsync(CancellationToken cancellationToken = default)
    {
        return await _context.Set<Booking>()
            .Include(b => b.User)
            .Include(b => b.Vehicle)
            .Include(b => b.Address)
            .Include(b => b.Ratings)
            .Include(b => b.Status)
            .Include(b => b.Notifications)
            .ToListAsync(cancellationToken);
    }

    public async Task<Booking?> GetByIdWithDetailsAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.Set<Booking>()
            .Include(b => b.User)
            .Include(b => b.Vehicle)
            .Include(b => b.Status)
            .Include(b => b.Address)
            .Include(b => b.Ratings)
            .Include(b => b.Notifications)
            .FirstOrDefaultAsync(b => b.Id == id, cancellationToken);
    }
}
