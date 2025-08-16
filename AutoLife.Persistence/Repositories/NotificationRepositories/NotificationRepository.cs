using AutoLife.Domain.Entities;
using AutoLife.Persistence.DataBaseContext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoLife.Persistence.Repositories.NotificationRepositories;

public class NotificationRepository(AppDbContext context) : GenericRepository<Notification, AppDbContext>(context), INotificationRepository
{
}
