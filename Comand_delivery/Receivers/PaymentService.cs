using System.Collections.Generic;

// Получатель - сервис оплаты
// Отвечает за финансовые операции: списание и возврат средств
public class PaymentService
{
    private readonly Dictionary<int, bool> _paidOrders = new();
    private readonly Dictionary<int, decimal> _amounts = new();

    // Метод списания средств
    public bool Charge(Order order)
    {
        Console.WriteLine($"ОПЛАТА:   Обрабатываю платёж для заказа #{order.Id}");
        Console.WriteLine($"ОПЛАТА:   Списание суммы: {order.TotalAmount:C}");

        // Сохраняем состояние оплаты для конкретного заказа
        _paidOrders[order.Id] = true;
        _amounts[order.Id] = order.TotalAmount;
        order.Status = "Оплачен";

        Console.WriteLine($"ОПЛАТА:   Платёж успешен!");
        return true;
    }

    // Метод возврата средств
    public void Refund(Order order)
    {
        if (_paidOrders.TryGetValue(order.Id, out bool isPaid) && isPaid)
        {
            Console.WriteLine($"ОПЛАТА:   Возврат средств за заказ #{order.Id}");
            Console.WriteLine($"ОПЛАТА:   Возвращаю сумму: {_amounts[order.Id]:C}");
            _paidOrders[order.Id] = false;
            order.Status = "Оплата возвращена";
        }
        else
        {
            Console.WriteLine($"ОПЛАТА:   Возврат не требуется - оплата не производилась для заказа #{order.Id}");
        }
    }

    // Геттер для проверки состояния оплаты конкретного заказа
    public bool IsPaid(int orderId) => _paidOrders.GetValueOrDefault(orderId, false);
}