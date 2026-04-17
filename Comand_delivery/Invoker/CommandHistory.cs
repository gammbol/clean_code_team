using System;
using System.Collections.Generic;
using System.Linq;

// История команд - стек выполненных команд для Undo/Redo
public class CommandHistory
{
    private readonly Stack<ICommand> _history;
    private readonly int _maxHistorySize;
    private readonly object _syncRoot = new object();

    public CommandHistory(int maxHistorySize = 50)
    {
        _history = new Stack<ICommand>();
        _maxHistorySize = maxHistorySize;
    }

    public void Push(ICommand command)
    {
        lock (_syncRoot)
        {
            if (_history.Count >= _maxHistorySize)
            {
                var tempArray = _history.ToArray();
                _history.Clear();
                for (int i = 0; i < tempArray.Length - 1; i++)
                {
                    _history.Push(tempArray[i]);
                }
            }
            _history.Push(command);
        }
        Console.WriteLine($"HISTORY:   Добавлена команда: {command.Description} (всего в истории: {_history.Count})");
    }

    public ICommand? Pop()
    {
        lock (_syncRoot)
        {
            if (_history.Count == 0)
            {
                Console.WriteLine("HISTORY:   История пуста - нечего отменять");
                return null;
            }
            var command = _history.Pop();
            Console.WriteLine($"HISTORY:   Извлечена команда для отмены: {command.Description} (осталось: {_history.Count})");
            return command;
        }
    }

    public void Clear()
    {
        lock (_syncRoot)
        {
            _history.Clear();
        }
        Console.WriteLine("HISTORY:   История очищена");
    }

    public int Count
    {
        get { lock (_syncRoot) return _history.Count; }
    }

    public bool CanUndo
    {
        get { lock (_syncRoot) return _history.Count > 0; }
    }
}