using NotificationApp.Core.Interfaces;
using NotificationApp.Core.Models;

namespace NotificationApp.Core.Services
{
    public class NotificationService
    {
        private INotificationStrategy _strategy;

        public NotificationService(INotificationStrategy strategy)
        {
            _strategy = strategy;
        }

        public void SetStrategy(INotificationStrategy strategy)
        {
            _strategy = strategy;
        }

        public void Send(Notification notification)
        {
            if (string.IsNullOrWhiteSpace(notification.Recipient))
                throw new ArgumentException("Recipient is required");

            if (string.IsNullOrWhiteSpace(notification.Message))
                throw new ArgumentException("Message is empty");

            _strategy.Send(notification);
        }
    }
}