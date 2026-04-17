// Конкретная команда - передача заказа на кухню
public class PrepareOrderCommand : ICommand
{
    private readonly KitchenService _kitchen;
    private readonly Order _order;
    // Сохраняем статус до выполнения для корректного отката
    private string _previousStatus;
    private bool _wasExecuted = false;

    public PrepareOrderCommand(KitchenService kitchen, Order order)
    {
        _kitchen = kitchen;
        _order = order;
    }

    public void Execute()
    {
        Console.WriteLine($"\n>>> ВЫПОЛНЕНИЕ: {Description}");
        _previousStatus = _order.Status; // Запоминаем контекст
        _kitchen.StartCooking(_order);
        _wasExecuted = true;
    }

    public void Undo()
    {
        if (_wasExecuted)
        {
            Console.WriteLine($"\n<<< ОТМЕНА: {Description}");
            _kitchen.CancelCooking(_order);
            _order.Status = _previousStatus; // Восстанавливаем исходный статус
            _wasExecuted = false;
        }
        else
        {
            Console.WriteLine($"INFO:  Команда '{Description}' не была выполнена, отмена не требуется");
        }
    }

    public string Description => $"Приготовить заказ #{_order.Id}";
}