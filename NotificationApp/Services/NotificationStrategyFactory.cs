using NotificationApp.Abstractions;
using NotificationApp.Strategies;

namespace NotificationApp.Services
{
    public class NotificationStrategyFactory
    {
        public INotificationStrategy GetStrategy(string type)
        {
            return type.ToLower() switch
            {
                "email" => new EmailNotificationStrategy(),
                "sms" => new SmsNotificationStrategy(),
                "push" => new PushNotificationStrategy(),
                _ => throw new ArgumentException("Unknown notification type")
            };
        }
    }
}