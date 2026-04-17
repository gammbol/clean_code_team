using System;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;

// Отправитель - диспетчер заказов
public class OrderDispatcher
{
    private readonly ConcurrentQueue<ICommand> _commandQueue = new();
    private readonly CommandHistory _history;
    private bool _isRunning;
    private Task? _workerTask;
    private CancellationTokenSource? _cancellationTokenSource;

    public OrderDispatcher()
    {
        _history = new CommandHistory(maxHistorySize: 100);
        _isRunning = false;
    }

    // Метод добавления команды в очередь 
    public void Enqueue(ICommand command)
    {
        Console.WriteLine($"\nDISPATCHER:     Получена команда: {command.Description}");
        _commandQueue.Enqueue(command);
        Console.WriteLine($"DISPATCHER:     Команда добавлена в очередь (в очереди: {_commandQueue.Count})");
    }

    public void StartWorker()
    {
        if (_isRunning)
        {
            Console.WriteLine("DISPATCHER:     Обработчик уже запущен");
            return;
        }
        Console.WriteLine("\nDISPATCHER:     Запуск фонового обработчика очереди...");
        _isRunning = true;
        _cancellationTokenSource = new CancellationTokenSource();
        _workerTask = Task.Run(() => ProcessQueueAsync(_cancellationTokenSource.Token));
        Console.WriteLine("DISPATCHER:     Обработчик запущен\n");
    }

    private async Task ProcessQueueAsync(CancellationToken cancellationToken)
    {
        while (!cancellationToken.IsCancellationRequested || !_commandQueue.IsEmpty)
        {
            // TryDequeue потокобезопасен
            if (_commandQueue.TryDequeue(out var command))
            {
                try
                {
                    command.Execute();
                    _history.Push(command);
                    await Task.Delay(500, cancellationToken);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"\nERROR:   Ошибка при выполнении команды: {ex.Message}");
                }
            }
            else
            {
                await Task.Delay(100, cancellationToken);
            }
        }
    }

    public void UndoLast()
    {
        Console.WriteLine("\nDISPATCHER:     Запрошена отмена последней операции...");
        var command = _history.Pop();
        if (command != null)
        {
            command.Undo();
            Console.WriteLine("DISPATCHER:     Последняя команда отменена\n");
        }
    }

    public void UndoMultiple(int count)
    {
        Console.WriteLine($"\nDISPATCHER:     Запрошена отмена {count} последних операций...");
        for (int i = 0; i < count; i++)
        {
            if (_history.CanUndo)
            {
                var command = _history.Pop();
                command?.Undo();
            }
            else
            {
                Console.WriteLine($"DISPATCHER:     Отменено {i} команд. Больше нет команд для отмены.");
                break;
            }
        }
        Console.WriteLine("DISPATCHER:     Множественная отмена завершена\n");
    }

    public void StopWorker()
    {
        if (!_isRunning)
        {
            Console.WriteLine("DISPATCHER:     Обработчик уже остановлен");
            return;
        }
        Console.WriteLine("\nDISPATCHER:     Остановка фонового обработчика...");
        _isRunning = false;
        _cancellationTokenSource?.Cancel();

        try
        {
            _workerTask?.Wait();
        }
        catch (AggregateException ex)
        {
            if (ex.InnerException is TaskCanceledException)
                Console.WriteLine("DISPATCHER:     Задача успешно отменена");
            else
                Console.WriteLine($"DISPATCHER:     Ошибка при остановке: {ex.InnerException?.Message}");
        }
        catch (TaskCanceledException)
        {
            Console.WriteLine("DISPATCHER:     Задача успешно отменена");
        }
        Console.WriteLine("DISPATCHER:     Обработчик остановлен\n");
    }

    public void PrintStatus()
    {
        Console.WriteLine("------------------\n");
        Console.WriteLine($"СТАТУС ДИСПЕТЧЕРА:");
        Console.WriteLine($"  Команд в очереди: {_commandQueue.Count}");
        Console.WriteLine($"  Команд в истории: {_history.Count}");
        Console.WriteLine($"  Обработчик активен: {_isRunning}");
        Console.WriteLine("-----------------\n");
    }

    public void ClearQueue()
    {
        while (!_commandQueue.IsEmpty) _commandQueue.TryDequeue(out _);
        Console.WriteLine("DISPATCHER:     Очередь очищена");
    }
}