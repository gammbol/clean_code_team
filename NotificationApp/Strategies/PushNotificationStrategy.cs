using NotificationApp.Core.Interfaces;
using NotificationApp.Core.Models;
using NotificationApp.Infrastructure;

namespace NotificationApp.Strategies
{
    public class PushNotificationStrategy : INotificationStrategy
    {
        private readonly PushProvider _provider;

        public PushNotificationStrategy(PushProvider provider)
        {
            _provider = provider;
        }

        public void Send(Notification notification)
        {
            _provider.SendPush(notification.Recipient, notification.Message);
        }
    }
}