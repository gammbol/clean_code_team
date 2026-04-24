namespace NotificationApp.Infrastructure
{
    public class SmsGateway
    {
        public void SendSms(string phone, string message)
        {
            Console.WriteLine($"[SMS] Отправка SMS на {phone}");
            Console.WriteLine($"Текст: {message}");
        }
    }
}