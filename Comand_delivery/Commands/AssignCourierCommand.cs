// Конкретная команда - назначение курьера
public class AssignCourierCommand : ICommand
{
    private readonly CourierService _courierService;
    private readonly Order _order;
    private string _previousStatus; // Контекст для отмены
    private bool _wasAssigned = false;

    public AssignCourierCommand(CourierService courierService, Order order)
    {
        _courierService = courierService;
        _order = order;
    }

    public void Execute()
    {
        Console.WriteLine($"\n>>> ВЫПОЛНЕНИЕ: {Description}");
        _previousStatus = _order.Status; // Запоминаем контекст
        _courierService.AssignCourier(_order);
        _wasAssigned = true;
    }

    public void Undo()
    {
        if (_wasAssigned)
        {
            Console.WriteLine($"\n<<< ОТМЕНА: {Description}");
            _courierService.CancelDelivery(_order);
            _order.Status = _previousStatus; // Восстанавливаем статус
            _wasAssigned = false;
        }
        else
        {
            Console.WriteLine($"INFO:  Курьер не был назначен, отмена не требуется");
        }
    }

    public string Description => $"Назначить курьера для заказа #{_order.Id}";
}