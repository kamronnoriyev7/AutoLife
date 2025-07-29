using AutoLife.Application.DTOs.BookingDTOs;
using AutoLife.Domain.Enums;

namespace AutoLife.Infrastructure.Services.BookingServices;

public interface IBookingService
{
    Task<List<BookingResponseDto>> GetAllAsync();
    Task<BookingResponseDto?> GetByIdAsync(Guid id);
    Task<BookingResponseDto> CreateAsync(BookingCreateDto dto);
    Task<BookingResponseDto?> UpdateAsync(Guid id, BookingUpdateDto dto);
    Task<bool> DeleteAsync(Guid id);
    Task<IEnumerable<BookingResponseDto>> GetAllByUserIdAsync(Guid userId, CancellationToken cancellationToken = default);
    Task<IEnumerable<BookingResponseDto>> GetAllWithDetailsAsync(CancellationToken cancellationToken = default);
}