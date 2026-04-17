// Конкретная команда - полная отмена заказа (компенсирующая транзакция)
public class CancelOrderCommand : ICommand
{
    private readonly PaymentService _paymentService;
    private readonly KitchenService _kitchen;
    private readonly CourierService _courierService;
    private readonly NotificationService _notificationService;
    private readonly Order _order;
    private bool _wasCancelled = false;
    private string _previousStatus;

    public CancelOrderCommand(
        PaymentService paymentService,
        KitchenService kitchen,
        CourierService courierService,
        NotificationService notificationService,
        Order order)
    {
        _paymentService = paymentService;
        _kitchen = kitchen;
        _courierService = courierService;
        _notificationService = notificationService;
        _order = order;
    }

    public void Execute()
    {
        Console.WriteLine($"\n>>> ВЫПОЛНЕНИЕ: {Description}");
        _previousStatus = _order.Status;

        // Компенсирующая логика: вызываем обратные методы сервисов
        _paymentService.Refund(_order);
        _kitchen.CancelCooking(_order);
        _courierService.CancelDelivery(_order);

        _notificationService.SendSms(_order, $"Заказ #{_order.Id} отменён. Средства возвращены.");
        _order.Status = "Отменён";
        _wasCancelled = true;
    }

    public void Undo()
    {
        if (_wasCancelled)
        {
            Console.WriteLine($"\n<<< ОТМЕНА: {Description}");
            Console.WriteLine($"INFO:  Восстановление статуса заказа: {_previousStatus}");
            _order.Status = _previousStatus; // Восстанавливаем состояние до отмены
            _wasCancelled = false;
        }
    }

    public string Description => $"Полностью отменить заказ #{_order.Id}";
}