using AutoLife.Domain.Entities;

namespace AutoLife.Infrastructure.Services.NotificationServices;

public interface INotificationService
{
    Task SendNotificationAsync(string title, string message, string? imageUrl = null, string? actionUrl = null);
    Task<IEnumerable<Notification>> GetNotificationsAsync(Guid userId);
    Task MarkNotificationAsReadAsync(Guid notificationId);
    Task DeleteNotificationAsync(Guid notificationId);
    Task ClearNotificationsAsync(Guid userId);
}