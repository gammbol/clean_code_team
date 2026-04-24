namespace NotificationApp.Infrastructure
{
    public class PushProvider
    {
        public void SendPush(string userId, string message)
        {
            Console.WriteLine($"[PUSH] Уведомление пользователю {userId}");
            Console.WriteLine($"Текст: {message}");
        }
    }
}