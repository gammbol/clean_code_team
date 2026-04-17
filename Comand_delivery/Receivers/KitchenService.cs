// Получатель (Receiver) - сервис кухни - приготовления еды
public class KitchenService
{
    // Метод начинает приготовление заказа
    public void StartCooking(Order order)
    {
        Console.WriteLine($"КУХНЯ:   Начинаю готовить заказ #{order.Id} для {order.CustomerName}");
        Console.WriteLine($"КУХНЯ:   Готовлю: сумма заказа {order.TotalAmount:C}");
        order.Status = "Готовится";
    }

    // Метод отменяет приготовление 
    public void CancelCooking(Order order)
    {
        Console.WriteLine($"КУХНЯ:   Отменяю приготовление заказа #{order.Id}");
        order.Status = "Отменён на кухне";
    }

    // Метод завершает приготовление
    public void FinishCooking(Order order)
    {
        Console.WriteLine($"КУХНЯ:   Заказ #{order.Id} готов к выдаче!");
        order.Status = "Готов";
    }
}