using NotificationApp.Core.Interfaces;
using NotificationApp.Core.Models;
using NotificationApp.Infrastructure;

namespace NotificationApp.Strategies
{
    public class SmsNotificationStrategy : INotificationStrategy
    {
        private readonly SmsGateway _gateway;

        public SmsNotificationStrategy(SmsGateway gateway)
        {
            _gateway = gateway;
        }

        public void Send(Notification notification)
        {
            _gateway.SendSms(notification.Recipient, notification.Message);
        }
    }
}