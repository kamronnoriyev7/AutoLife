using AutoLife.Domain.Entities;
using AutoLife.Persistence.DataBaseContext;

namespace AutoLife.Persistence.Repositories.NotificationRepositories;

public interface INotificationRepository : IGenericRepository<Notification, AppDbContext>
{
}
