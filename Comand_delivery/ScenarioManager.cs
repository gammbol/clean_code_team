using System;
using System.Threading;

// Отвечает за создание всех компонентов системы и запуск сценариев
public class ScenarioManager
{
    // сервисы с бизнес-логикой
    private readonly KitchenService _kitchen;
    private readonly PaymentService _payment;
    private readonly CourierService _courier;
    private readonly NotificationService _notification;

    // диспетчер очереди и истории
    private readonly OrderDispatcher _dispatcher;

    private Order _order1;
    private Order _order2;
    private Order _order3;

    public ScenarioManager()
    {
        _kitchen = new KitchenService();
        _payment = new PaymentService();
        _courier = new CourierService();
        _notification = new NotificationService();
        _dispatcher = new OrderDispatcher();
    }


    public void Run()
    {
        Console.WriteLine("СИСТЕМА ОБРАБОТКИ ЗАКАЗОВ ДОСТАВКИ");
        Console.WriteLine("Демонстрация паттерна Command\n");
        PrintSeparator();

        // Запускаем фоновый обработчик очереди
        _dispatcher.StartWorker();

        // Запускаем сценарии 
        RunScenario1_SingleCommands();
        RunScenario2_MacroCommand();
        RunScenario3_UndoOperations();
        RunScenario4_UndoMacroCommand();
        RunScenario5_ComplexCancellation();

        PrintFinalStats();

        // останавливаем диспетчер
        _dispatcher.StopWorker();
    }

    // Сценарий 1: Демонстрация отдельных команд в очереди
    private void RunScenario1_SingleCommands()
    {
        Console.WriteLine("\nСЦЕНАРИЙ 1: Отдельные команды в очереди");
        PrintSeparator();

        _order1 = new Order(1, "Иванов Иван", "ул. Ленина 10, кв. 5", 1500);
        Console.WriteLine($"Создан заказ: {_order1}\n");

        var chargeCmd = new ChargePaymentCommand(_payment, _order1);
        var prepareCmd = new PrepareOrderCommand(_kitchen, _order1);

        Console.WriteLine("CLIENT:   Отправка команд в очередь...");
        _dispatcher.Enqueue(chargeCmd);
        _dispatcher.Enqueue(prepareCmd);

        WaitForProcessing(2);
    }

    // Сценарий 2: Макрокоманда группировка команд 
    private void RunScenario2_MacroCommand()
    {
        Console.WriteLine("\nСЦЕНАРИЙ 2: Макрокоманда (пакетная операция)");
        PrintSeparator();

        _order2 = new Order(2, "Петрова Мария", "пр. Мира 25, офис 301", 2800);
        Console.WriteLine($"Создан заказ: {_order2}\n");

        var processOrderMacro = new MacroCommand(
            "Полная обработка заказа #2",
            new ChargePaymentCommand(_payment, _order2),
            new PrepareOrderCommand(_kitchen, _order2),
            new AssignCourierCommand(_courier, _order2)
        );

        _dispatcher.Enqueue(processOrderMacro);
        WaitForProcessing(3);
    }

    // Сценарий 3: Отмена операций (Undo)
    private void RunScenario3_UndoOperations()
    {
        Console.WriteLine("\nСЦЕНАРИЙ 3: Отмена последних операций (Undo)");
        PrintSeparator();
        Console.WriteLine("CLIENT:   Отменяем 2 последние операции...");
        _dispatcher.UndoLast();
        WaitForProcessing(1);
        _dispatcher.UndoLast();
        WaitForProcessing(1);
    }

    // Сценарий 4: Отмена макрокоманды целиком
    private void RunScenario4_UndoMacroCommand()
    {
        Console.WriteLine("\nСЦЕНАРИЙ 4: Отмена макрокоманды (полный откат)");
        PrintSeparator();

        _order3 = new Order(3, "Сидоров Алексей", "ул. Гагарина 8", 950);
        Console.WriteLine($"Создан заказ: {_order3}\n");

        var processOrderMacro = new MacroCommand(
            "Полная обработка заказа #3",
            new ChargePaymentCommand(_payment, _order3),
            new PrepareOrderCommand(_kitchen, _order3),
            new AssignCourierCommand(_courier, _order3)
        );

        _dispatcher.Enqueue(processOrderMacro);
        WaitForProcessing(3);

        // если макрокоманда частично выполнилась, Undo откатывает всё в обратном порядке
        Console.WriteLine("\nCLIENT:   Отмена всей макрокоманды...");
        _dispatcher.UndoLast();
        WaitForProcessing(2);
    }

    // Сценарий 5: полная отмена заказа
    private void RunScenario5_ComplexCancellation()
    {
        Console.WriteLine("\nСЦЕНАРИЙ 5: Специальная команда отмены заказа");
        PrintSeparator();

        var completeOrder1 = new MacroCommand(
            "Завершение заказа #1",
            new AssignCourierCommand(_courier, _order1),
            new PrepareOrderCommand(_kitchen, _order1)
        );
        _dispatcher.Enqueue(completeOrder1);
        WaitForProcessing(2);

        // Команда отмены работает с тем же объектом _order1
        var cancelOrderCmd = new CancelOrderCommand(
            _payment, _kitchen, _courier, _notification,
            _order1 // Передаём ссылку на существующий заказ
        );
        Console.WriteLine("CLIENT:   Полная отмена заказа с компенсацией...");
        _dispatcher.Enqueue(cancelOrderCmd);
        WaitForProcessing(2);
    }


    private void WaitForProcessing(int seconds)
    {
        Thread.Sleep(seconds * 1000);
    }

    private void PrintSeparator()
    {
        Console.WriteLine(new string('-', 30));
    }

    private void PrintFinalStats()
    {
        Console.WriteLine("\nФИНАЛЬНАЯ СТАТИСТИКА:");
        PrintSeparator();
        _dispatcher.PrintStatus();
    }
}