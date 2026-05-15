using System;
using System.Collections.Generic;
using System.Threading;

// Отвечает за создание всех компонентов системы и управление заказами
public class ScenarioManager
{
    // Сервисы с бизнес-логикой (Receivers)
    private readonly KitchenService _kitchen;
    private readonly PaymentService _payment;
    private readonly CourierService _courier;
    private readonly NotificationService _notification;

    // Диспетчер очереди и истории (Invoker)
    private readonly OrderDispatcher _dispatcher;

    // Счётчик заказов для генерации ID
    private int _orderCounter = 0;

    // Список созданных заказов (для отображения истории)
    private readonly List<Order> _orders = new();

    public ScenarioManager()
    {
        _kitchen = new KitchenService();
        _payment = new PaymentService();
        _courier = new CourierService();
        _notification = new NotificationService();
        _dispatcher = new OrderDispatcher();
    }

    // Запуск системы - диспетчер работает непрерывно
    public void Run()
    {
        Console.WriteLine("\nЗАПУСК СИСТЕМЫ...\n");

        // Запускаем фоновый обработчик очереди. Он будет работать постоянно, пока не вызовем Stop()
        _dispatcher.StartWorker();

        Console.WriteLine("Система готова к приёму заказов");
        Console.WriteLine("Диспетчер работает в фоновом режиме\n");
    }

    // Метод 1: Создать простой заказ (отдельные команды)
    public void CreateSimpleOrder()
    {
        _orderCounter++;
        var order = new Order(
            _orderCounter,
            $"Клиент {_orderCounter}",
            $"Адрес {_orderCounter}",
            1000 + (_orderCounter * 100)
        );

        _orders.Add(order);

        Console.WriteLine($"\nСОЗДАН ЗАКАЗ #{order.Id}");
        Console.WriteLine(order);

        // Создаём команды
        var chargeCmd = new ChargePaymentCommand(_payment, order);
        var prepareCmd = new PrepareOrderCommand(_kitchen, order);

        // Отправляем в очередь
        Console.WriteLine("\nОтправка команд в очередь...");
        _dispatcher.Enqueue(chargeCmd);
        _dispatcher.Enqueue(prepareCmd);

        Console.WriteLine("Заказ добавлен в очередь обработки");
    }

    // Метод 2: Создать заказ с полной обработкой (макрокоманда)
    public void CreateFullOrder()
    {
        _orderCounter++;
        var order = new Order(
            _orderCounter,
            $"VIP Клиент {_orderCounter}",
            $"VIP Адрес {_orderCounter}",
            2500 + (_orderCounter * 200)
        );

        _orders.Add(order);

        Console.WriteLine($"\nСОЗДАН VIP ЗАКАЗ #{order.Id}");
        Console.WriteLine(order);

        // Создаём макрокоманду "Полная обработка"
        var fullProcessMacro = new MacroCommand(
            $"Полная обработка заказа #{order.Id}",
            new ChargePaymentCommand(_payment, order),
            new PrepareOrderCommand(_kitchen, order),
            new AssignCourierCommand(_courier, order)
        );

        // Отправляем макрокоманду в очередь
        Console.WriteLine("\nОтправка макрокоманды в очередь...");
        _dispatcher.Enqueue(fullProcessMacro);

        Console.WriteLine("VIP заказ добавлен в очередь");
    }

    // Метод 3: Отменить последнюю операцию
    public void UndoLastOperation()
    {
        Console.WriteLine("\nОтмена последней операции...");
        _dispatcher.UndoLast();
    }

    // Метод 4: Показать статус системы
    public void PrintStatus()
    {
        Console.WriteLine("\nСТАТУС СИСТЕМЫ:");
        _dispatcher.PrintStatus();
    }

    // Метод 5: Показать историю заказов
    public void PrintOrderHistory()
    {
        Console.WriteLine("\nИСТОРИЯ ЗАКАЗОВ:");
        Console.WriteLine(new string('-', 50));

        if (_orders.Count == 0)
        {
            Console.WriteLine("Пока нет заказов");
        }
        else
        {
            foreach (var order in _orders)
            {
                Console.WriteLine(order);
            }
        }

        Console.WriteLine(new string('-', 50));
        Console.WriteLine($"Всего заказов: {_orders.Count}");
    }

    // Метод 6: Демо-сценарий (несколько заказов подряд)
    public void RunDemoScenario()
    {
        Console.WriteLine("\nЗАПУСК ДЕМО-СЦЕНАРИЯ (3 заказа подряд)...\n");

        // Заказ 1: Простой
        CreateSimpleOrder();
        Thread.Sleep(1000);

        // Заказ 2: VIP
        CreateFullOrder();
        Thread.Sleep(1000);

        // Заказ 3: Ещё один простой
        CreateSimpleOrder();
        Thread.Sleep(1000);

        Console.WriteLine("\nДемо-сценарий завершён");
        Console.WriteLine("Система продолжает работу и ждёт новые заказы...\n");
    }

    // Остановка системы 
    public void Stop()
    {
        _dispatcher.StopWorker();
    }
}
