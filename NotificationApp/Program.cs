using NotificationApp.Core.Models;
using NotificationApp.Core.Services;
using NotificationApp.Strategies;
using NotificationApp.Infrastructure;

namespace NotificationApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var emailClient = new EmailClient();
            var smsGateway = new SmsGateway();
            var pushProvider = new PushProvider();

            var notification = new Notification
            {
                Recipient = "user@test.com",
                Message = "Добро пожаловать в систему!"
            };

            // выбираем стратегию
            var emailStrategy = new EmailNotificationStrategy(emailClient);
            var service = new NotificationService(emailStrategy);

            service.Send(notification);

            Console.WriteLine("\nСмена стратегии...\n");

            // меняем стратегию на лету
            service.SetStrategy(new SmsNotificationStrategy(smsGateway));
            service.Send(notification);

            Console.WriteLine("\nЕще смена...\n");

            service.SetStrategy(new PushNotificationStrategy(pushProvider));
            service.Send(notification);
        }
    }
}