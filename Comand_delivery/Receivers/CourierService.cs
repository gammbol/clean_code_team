// Получатель - сервис курьерской доставки
// Отвечает за назначение курьера и доставку заказа
public class CourierService
{
    private string _currentCourier = "Не назначен";
    private bool _isAssigned = false;

    // Метод назначения курьера на заказ
    public void AssignCourier(Order order)
    {
        Console.WriteLine($"КУРЬЕР:   Ищу свободного курьера для заказа #{order.Id}");

        // Имитация поиска курьера
        _currentCourier = "Курьер Иван (ID: 42)";
        _isAssigned = true;

        Console.WriteLine($"КУРЬЕР:   Курьер {_currentCourier} назначен на заказ #{order.Id}");
        Console.WriteLine($"КУРЬЕР:   Адрес доставки: {order.Address}");
        order.Status = "Курьер назначен";
    }

    // Метод отмены назначения курьера (для Undo)
    public void CancelDelivery(Order order)
    {
        if (_isAssigned)
        {
            Console.WriteLine($"КУРЬЕР:   Отменяю доставку заказа #{order.Id}");
            Console.WriteLine($"КУРЬЕР:   Курьер {_currentCourier} освобождён");
            _isAssigned = false;
            _currentCourier = "Не назначен";
            order.Status = "Доставка отменена";
        }
        else
        {
            Console.WriteLine($"КУРЬЕР:   Курьер ещё не был назначен на заказ #{order.Id}");
        }
    }

    // Метод начала доставки
    public void StartDelivery(Order order)
    {
        Console.WriteLine($"КУРЬЕР:   Курьер {_currentCourier} выехал к клиенту с заказом #{order.Id}");
        order.Status = "В пути";
    }

    // Метод завершения доставки
    public void CompleteDelivery(Order order)
    {
        Console.WriteLine($"КУРЬЕР:   Заказ #{order.Id} доставлен клиенту {order.CustomerName}!");
        order.Status = "Доставлен";
    }
}