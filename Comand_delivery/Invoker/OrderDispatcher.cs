using System;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;

// Invoker - диспетчер заказов
public class OrderDispatcher
{
    // Потокобезопасная очередь команд
    private readonly ConcurrentQueue<ICommand> _commandQueue = new();

    // История выполненных команд для Undo
    private readonly CommandHistory _history;

    // Флаг работы системы
    private bool _isRunning;

    // Задача фонового обработчика
    private Task? _workerTask;

    // Токен для остановки
    private CancellationTokenSource? _cancellationTokenSource;

    // Счётчик выполненных команд
    private int _commandsExecuted = 0;

    public OrderDispatcher()
    {
        _history = new CommandHistory(maxHistorySize: 100);
        _isRunning = false;
    }

    // Добавление команды в очередь
    public void Enqueue(ICommand command)
    {
        Console.WriteLine($"\nDISPATCHER: Получена команда: {command.Description}");
        _commandQueue.Enqueue(command);
        Console.WriteLine($"DISPATCHER: Команда в очереди (всего: {_commandQueue.Count})");
    }

    // Запуск непрерывного обработчика
    public void StartWorker()
    {
        if (_isRunning)
        {
            Console.WriteLine("Обработчик уже запущен");
            return;
        }

        Console.WriteLine("Запуск фонового обработчика очереди...");
        _isRunning = true;
        _cancellationTokenSource = new CancellationTokenSource();

        // Запускаем фоновую задачу
        _workerTask = Task.Run(() => ProcessQueueAsync(_cancellationTokenSource.Token));

        Console.WriteLine("Обработчик запущен и готов к работе\n");
    }

    // Непрерывная обработка очереди
    private async Task ProcessQueueAsync(CancellationToken cancellationToken)
    {
        Console.WriteLine("Обработчик слушает очередь...\n");

        // Цикл работает ПОСТОЯННО, пока не вызовут Stop()
        while (!cancellationToken.IsCancellationRequested)
        {
            // Пытаемся взять команду из очереди
            if (_commandQueue.TryDequeue(out var command))
            {
                try
                {
                    // Выполняем команду
                    await ExecuteCommandAsync(command, cancellationToken);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"\nERROR: Ошибка при выполнении: {ex.Message}");
                }
            }
            else
            {
                // Если очередь пуста - ждём немного и проверяем снова. Система не завершается, а продолжает работать
                await Task.Delay(100, cancellationToken);
            }
        }
    }

    // Асинхронное выполнение команды
    private async Task ExecuteCommandAsync(ICommand command, CancellationToken cancellationToken)
    {
        Console.WriteLine($"\nВЫПОЛНЕНИЕ: {command.Description}");

        try
        {
            // Выполняем команду
            command.Execute();

            // Добавляем в историю для Undo
            _history.Push(command);

            // Увеличиваем счётчик
            _commandsExecuted++;

            // Имитация обработки
            await Task.Delay(300, cancellationToken);

            Console.WriteLine($"Команда выполнена (всего выполнено: {_commandsExecuted})");
        }
        catch (OperationCanceledException)
        {
            Console.WriteLine($"Команда отменена: {command.Description}");
            throw;
        }
    }

    // Отмена последней операции
    public void UndoLast()
    {
        Console.WriteLine("\nDISPATCHER: Отмена последней операции...");

        var command = _history.Pop();
        if (command != null)
        {
            command.Undo();
            Console.WriteLine("Последняя команда отменена");
        }
    }

    // Остановка системы (только при выходе)
    public void StopWorker()
    {
        if (!_isRunning)
        {
            Console.WriteLine("Обработчик уже остановлен");
            return;
        }

        Console.WriteLine("\nОстановка системы...");
        _isRunning = false;

        // Отменяем токен
        _cancellationTokenSource?.Cancel();

        // Ждём завершения задачи
        try
        {
            _workerTask?.Wait();
        }
        catch (AggregateException ex)
        {
            if (ex.InnerException is TaskCanceledException)
            {
                Console.WriteLine("Задача успешно остановлена");
            }
        }
        catch (TaskCanceledException)
        {
            Console.WriteLine("Задача успешно остановлена");
        }

        Console.WriteLine($"Итого выполнено команд: {_commandsExecuted}");
        Console.WriteLine("Система остановлена\n");
    }


    // Показать статус
    public void PrintStatus()
    {
        Console.WriteLine($"СТАТУС ДИСПЕТЧЕРА:");
        Console.WriteLine($"Команд в очереди: {_commandQueue.Count}");
        Console.WriteLine($"Команд в истории: {_history.Count}");
        Console.WriteLine($"Выполнено команд: {_commandsExecuted}");
        Console.WriteLine($"Система активна: {_isRunning}");
    }
}
