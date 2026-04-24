namespace NotificationApp.Infrastructure
{
    public class EmailClient
    {
        public void SendEmail(string to, string message)
        {
            Console.WriteLine($"[EMAIL] Отправка письма на {to}");
            Console.WriteLine($"Текст: {message}");
        }
    }
}