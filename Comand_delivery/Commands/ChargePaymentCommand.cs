// Конкретная команда - оплата заказа
public class ChargePaymentCommand : ICommand
{
    private readonly PaymentService _paymentService;
    private readonly Order _order;
    private string _previousStatus; // Контекст для отмены
    private bool _wasPaid = false;

    public ChargePaymentCommand(PaymentService paymentService, Order order)
    {
        _paymentService = paymentService;
        _order = order;
    }

    public void Execute()
    {
        Console.WriteLine($"\n>>> ВЫПОЛНЕНИЕ: {Description}");
        _previousStatus = _order.Status; // Запоминаем контекст
        _wasPaid = _paymentService.Charge(_order);
    }

    public void Undo()
    {
        if (_wasPaid)
        {
            Console.WriteLine($"\n<<< ОТМЕНА: {Description}");
            _paymentService.Refund(_order);
            _order.Status = _previousStatus; // Восстанавливаем статус
            _wasPaid = false;
        }
        else
        {
            Console.WriteLine($"INFO:  Оплата не была произведена, возврат не требуется");
        }
    }

    public string Description => $"Оплатить заказ #{_order.Id} ({_order.TotalAmount:C})";
}