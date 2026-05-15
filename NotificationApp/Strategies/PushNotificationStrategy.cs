using NotificationApp.Abstractions;
using NotificationApp.Models;

namespace NotificationApp.Strategies
{
    public class PushNotificationStrategy : INotificationStrategy
    {
        public Task SendAsync(NotificationRequest request)
        {
            Console.WriteLine($"[PUSH] to {request.UserId}: {request.Message}");
            return Task.CompletedTask;
        }
    }
}