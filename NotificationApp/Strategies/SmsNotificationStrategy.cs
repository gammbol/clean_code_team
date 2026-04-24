using NotificationApp.Abstractions;
using NotificationApp.Models;

namespace NotificationApp.Strategies
{
    public class SmsNotificationStrategy : INotificationStrategy
    {
        public Task SendAsync(NotificationRequest request)
        {
            Console.WriteLine($"[SMS] to {request.UserId}: {request.Message}");
            return Task.CompletedTask;
        }
    }
}