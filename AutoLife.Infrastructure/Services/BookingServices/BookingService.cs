using AutoLife.Application.DTOs.BookingDTOs;
using AutoLife.Domain.Entities;
using AutoLife.Domain.Enums;
using AutoLife.Persistence.DataBaseContext;
using AutoLife.Persistence.Repositories;
using AutoLife.Persistence.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using SendGrid.Helpers.Errors.Model;

namespace AutoLife.Infrastructure.Services.BookingServices;

internal class BookingService : IBookingService
{
    private readonly IUnitOfWork<AppDbContext> _unitOfWork;
    private readonly IGenericRepository<Booking> _bookingRepository;

    public BookingService(IUnitOfWork<AppDbContext> unitOfWork, IGenericRepository<Booking> bookingRepository)
    {
        _unitOfWork = unitOfWork;
        _bookingRepository = bookingRepository;
    }

    public async Task<BookingResponseDto> CreateAsync(BookingCreateDto dto)
    {
        if(dto == null)
            throw new ArgumentNullException(nameof(dto), "BookingCreateDto cannot be null");

        var booking = new Booking
        {
            Id = Guid.NewGuid(),
            UserId = dto.UserId,
            AddressId = dto.AddressId,
            TargetId = dto.TargetId,
            Description = dto.Description,
            BookingType = dto.BookingType,
            CreateDate = DateTime.UtcNow,
            From = dto.From,
            To = dto.To,
            Status = BookingStatus.Confirmed,
            TotalPrice = dto.TotalPrice,
            VehicleId = dto.VehicleId,
        };

        var bookingResponseDto = new BookingResponseDto
        {
            Id = booking.Id,
            UserFullName = string.Empty, // This should be set based on the user data
            BookingType = booking.BookingType,
            TargetId = booking.TargetId,
            Description = booking.Description,
            From = booking.From,
            To = booking.To,
            SpotCount = dto.SpotCount,
            TotalPrice = booking.TotalPrice,
            Status = booking.Status
        };

        await _bookingRepository.AddAsync(booking);
        await _unitOfWork.SaveChangesAsync();
        return bookingResponseDto;
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
       if (id == Guid.Empty)
            throw new ArgumentException("Invalid booking ID", nameof(id));

        var booking = await _bookingRepository.GetByIdAsync(id);
        if (booking == null)
            throw new NotFoundException("Booking not found!");

        booking.IsDeleted = true; 
        booking.DeleteDate = DateTime.UtcNow;

        await _bookingRepository.SoftDeleteAsync(booking.Id);
        await _unitOfWork.SaveChangesAsync();
        return true;
    }

    public async Task<List<BookingResponseDto>> GetAllAsync()
    {
        var bookings = await _bookingRepository.GetAllAsync();

        var bookingDtos = bookings.Select(b => MapToResponseDto(b)).ToList();
        return bookingDtos;

    }

    public async Task<IEnumerable<BookingResponseDto>?> GetAllByUserIdAsync(Guid userId, CancellationToken cancellationToken = default)
    {
        return null;
            
    }

    public async Task<IEnumerable<BookingResponseDto>> GetAllWithDetailsAsync(CancellationToken cancellationToken = default)
    {
        var bookings = await _bookingRepository.GetPagedListAsync(1, 1000, asNoTracking: true, includeProperties: "User,Vehicle,Parking,FuelStation,ServiceCenter,Address");
        
        if (bookings == null || !bookings.Any())
            return Enumerable.Empty<BookingResponseDto>();
        
       var bookingDtos = bookings.Select(b => new BookingResponseDto
        {
            Id = b.Id,
            UserFullName = $"{b.User.FirstName} {b.User.LastName}",
            BookingType = b.BookingType,
            TargetId = b.TargetId,
            Description = b.Description,
            From = b.From,
            To = b.To,
            SpotCount = b.SpotCount,
            TotalPrice = b.TotalPrice,
            Status = b.Status
        }).ToList();
        return bookingDtos;
    }

    public async Task<BookingResponseDto?> GetByIdAsync(Guid id)
    {
        var booking = await _bookingRepository.GetByIdAsync(id)
            ?? throw new NotFoundException("Booking not found!");

       var bookingDto = new BookingResponseDto
        {
            Id = booking.Id,
            UserFullName = $"{booking.User.FirstName} {booking.User.LastName}",
            BookingType = booking.BookingType,
            TargetId = booking.TargetId,
            Description = booking.Description,
            From = booking.From,
            To = booking.To,
            SpotCount = booking.SpotCount,
            TotalPrice = booking.TotalPrice,
            Status = booking.Status
        };
        return bookingDto;
    }

    public async Task<BookingResponseDto?> UpdateAsync(Guid id, BookingUpdateDto dto)
    {
        if (id == Guid.Empty)
            throw new ArgumentException("Invalid booking ID", nameof(id));
        
        if (dto == null)
            throw new ArgumentNullException(nameof(dto), "BookingUpdateDto cannot be null");

        var booking = await _bookingRepository.GetByIdAsync(id);
        if (booking == null)
            throw new NotFoundException("Booking not found!");

        booking.Description = dto.Description;
        booking.From = dto.From;
        booking.To = dto.To;
        booking.SpotCount = dto.SpotCount;
        booking.TotalPrice = dto.TotalPrice;

        _bookingRepository.Update(booking);
        await _unitOfWork.SaveChangesAsync();
        return MapToResponseDto(booking);
    }

    private BookingResponseDto MapToResponseDto(Booking booking)
    {
        return new BookingResponseDto
        {
            Id = booking.Id,
            BookingType = booking.BookingType,
            TargetId = booking.TargetId,
            Description = booking.Description,
            From = booking.From,
            To = booking.To,
            SpotCount = booking.SpotCount,
            TotalPrice = booking.TotalPrice,
            Status = booking.Status
        };
    }
}
