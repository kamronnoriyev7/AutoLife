using AutoLife.Domain.Entities;
using AutoLife.Infrastructure.Services.NotificationServices;
using Microsoft.AspNetCore.Mvc;

namespace AutoLife.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class NotificationController : ControllerBase
{
    private readonly INotificationService _notificationService;

    public NotificationController(INotificationService notificationService)
    {
        _notificationService = notificationService;
    }

    [HttpGet("{userId}")]
    public async Task<IActionResult> GetUserNotifications(Guid userId)
    {
        var notifications = await _notificationService.GetNotificationsAsync(userId);
        return Ok(notifications);
    }

    [HttpPost("send")]
    public async Task<IActionResult> SendNotification([FromQuery] Guid userId, [FromQuery] string title, [FromQuery] string message)
    {
        await _notificationService.SendNotificationAsync(title, message, null, null);
        return Ok("Notification sent successfully.");
    }

    [HttpPut("mark-as-read/{id}")]
    public async Task<IActionResult> MarkAsRead(Guid id)
    {
        await _notificationService.MarkNotificationAsReadAsync(id);
        return Ok("Notification marked as read.");
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteNotification(Guid id)
    {
        await _notificationService.DeleteNotificationAsync(id);
        return Ok("Notification deleted.");
    }

    [HttpDelete("clear/{userId}")]
    public async Task<IActionResult> ClearUserNotifications(Guid userId)
    {
        await _notificationService.ClearNotificationsAsync(userId);
        return Ok("All notifications cleared for the user.");
    }
}
