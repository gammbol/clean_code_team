// Получатель  - сервис уведомлений
// Отправляет SMS и email клиентам о статусе заказа
public class NotificationService
{
    // Отправка SMS уведомления
    public void SendSms(Order order, string message)
    {
        Console.WriteLine($"SMS:   Отправка на телефон клиента {order.CustomerName}");
        Console.WriteLine($"SMS:   Текст: {message}");
    }

    // Отправка email уведомления
    public void SendEmail(Order order, string subject, string body)
    {
        Console.WriteLine($"EMAIL:   Отправка на email клиента {order.CustomerName}");
        Console.WriteLine($"EMAIL:   Тема: {subject}");
        Console.WriteLine($"EMAIL:   Текст: {body}");
    }

    // Уведомление о подтверждении заказа
    public void NotifyOrderConfirmed(Order order)
    {
        SendSms(order, $"Ваш заказ #{order.Id} подтверждён. Сумма: {order.TotalAmount:C}");
    }

    // Уведомление о доставке
    public void NotifyOrderDelivered(Order order)
    {
        SendEmail(order, "Заказ доставлен!", $"Спасибо за заказ! Заказ #{order.Id} доставлен.");
    }
}