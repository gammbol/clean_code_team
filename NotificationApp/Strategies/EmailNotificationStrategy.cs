using NotificationApp.Abstractions;
using NotificationApp.Models;

namespace NotificationApp.Strategies
{
    public class EmailNotificationStrategy : INotificationStrategy
    {
        public Task SendAsync(NotificationRequest request)
        {
            Console.WriteLine($"[EMAIL] to {request.UserId}: {request.Message}");
            return Task.CompletedTask;
        }
    }
}