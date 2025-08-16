using AutoLife.Domain.Entities;
using AutoLife.Persistence.DataBaseContext;
using AutoLife.Persistence.Repositories;
using AutoLife.Persistence.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoLife.Infrastructure.Services.NotificationServices;

public class NotificationService : INotificationService
{
    private readonly IUnitOfWork<AppDbContext> _unitOfWork;
    private readonly IGenericRepository<Notification, AppDbContext> _notificationRepository;

    public NotificationService(IUnitOfWork<AppDbContext> unitOfWork, IGenericRepository<Notification, AppDbContext> notificationRepository)
    {
        _unitOfWork = unitOfWork;
        _notificationRepository = notificationRepository;
    }

    public async Task ClearNotificationsAsync(Guid userId)
    {
        if (userId == Guid.Empty)
            throw new ArgumentException("User ID cannot be empty.", nameof(userId));

        var notifications = await _notificationRepository.FindAsync(n => n.UserId == userId && !n.IsDeleted);
        foreach (var notification in notifications)
        {
            notification.IsDeleted = true;
            notification.DeleteDate = DateTime.UtcNow;
        }
        await _unitOfWork.SaveChangesAsync();
    }

    public async Task DeleteNotificationAsync(Guid notificationId)
    {
        if (notificationId == Guid.Empty)
            throw new ArgumentException("Notification ID cannot be empty.", nameof(notificationId));
        var notification = await _notificationRepository.GetByIdAsync(notificationId, asNoTracking: true);
        if (notification == null)
            throw new KeyNotFoundException($"Notification with ID {notificationId} not found.");

        notification.IsDeleted = true;
        notification.DeleteDate = DateTime.UtcNow;
        await _notificationRepository.SoftDeleteAsync(notification.Id);
        await _unitOfWork.SaveChangesAsync();
    }

    public async Task<IEnumerable<Notification>> GetNotificationsAsync(Guid userId)
    {
        if (userId == Guid.Empty)
            throw new ArgumentException("User ID cannot be empty.", nameof(userId));
        var notifications = await _notificationRepository.FindAsync(n => n.UserId == userId && !n.IsDeleted);
        return notifications.OrderByDescending(n => n.CreateDate).ToList();
    }

    public async Task MarkNotificationAsReadAsync(Guid notificationId)
    {
        if (notificationId == Guid.Empty)
            throw new ArgumentException("Notification ID cannot be empty.", nameof(notificationId));
        var notification = await _notificationRepository.GetByIdAsync(notificationId, asNoTracking: true);
        if (notification == null)
            throw new KeyNotFoundException($"Notification with ID {notificationId} not found.");

        notification.IsRead = true;

        _notificationRepository.Update(notification);
        await _unitOfWork.SaveChangesAsync();
    }

    public async Task SendNotificationAsync(string title, string message, string? imageUrl = null, string? actionUrl = null)
    {
        if (string.IsNullOrWhiteSpace(title))
            throw new ArgumentException("Notification title cannot be empty.", nameof(title));
        if (string.IsNullOrWhiteSpace(message))
            throw new ArgumentException("Notification message cannot be empty.", nameof(message));
        var notification = new Notification
        {
            Id = Guid.NewGuid(),
            Title = title,
            CreateDate = DateTime.UtcNow,
            IsRead = false,
            IsDeleted = false
        };
        await _notificationRepository.AddAsync(notification);
        await _unitOfWork.SaveChangesAsync();
    }
}
