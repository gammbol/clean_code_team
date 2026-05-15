using NotificationApp.Models;

namespace NotificationApp.Abstractions
{
    public interface INotificationStrategy
    {
        Task SendAsync(NotificationRequest request);
    }
}