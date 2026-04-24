using NotificationApp.Core.Models;

namespace NotificationApp.Core.Interfaces
{
    public interface INotificationStrategy
    {
        void Send(Notification notification);
    }
}