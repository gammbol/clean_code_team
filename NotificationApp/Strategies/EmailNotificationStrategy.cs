using NotificationApp.Core.Interfaces;
using NotificationApp.Core.Models;
using NotificationApp.Infrastructure;

namespace NotificationApp.Strategies
{
    public class EmailNotificationStrategy : INotificationStrategy
    {
        private readonly EmailClient _client;

        public EmailNotificationStrategy(EmailClient client)
        {
            _client = client;
        }

        public void Send(Notification notification)
        {
            _client.SendEmail(notification.Recipient, notification.Message);
        }
    }
}