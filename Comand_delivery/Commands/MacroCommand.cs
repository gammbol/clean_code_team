// Макрокоманда - команда, содержащая другие команды
public class MacroCommand : ICommand
{
    // Список подчинённых команд
    private readonly List<ICommand> _commands;

    // Стек для отслеживания выполненных команд 
    private readonly Stack<ICommand> _executedCommands;

    // Описание макрокоманды
    private readonly string _description;

    public MacroCommand(string description, params ICommand[] commands)
    {
        _description = description;
        _commands = new List<ICommand>(commands);
        _executedCommands = new Stack<ICommand>();
    }

    // Выполнение всех команд по очереди
    public void Execute()
    {
        Console.WriteLine("\n----------------");
        Console.WriteLine($">>> МАКРОКОМАНДА: {_description}");
        Console.WriteLine("------------------\n");

        foreach (var command in _commands)
        {
            try
            {
                command.Execute();
                _executedCommands.Push(command); // Запоминаем выполненную команду
            }
            catch (Exception ex)
            {
                // Если команда упала, отменяем все предыдущие
                Console.WriteLine($"\nERROR:   Ошибка при выполнении команды '{command.Description}': {ex.Message}");
                Console.WriteLine("ERROR:   Откатываю выполненные команды...");
                Undo();
                throw; // Пробрасываем ошибку дальше
            }
        }

        Console.WriteLine("\n----------------");
        Console.WriteLine($">>> МАКРОКОМАНДА '{_description}' УСПЕШНО ВЫПОЛНЕНА");
        Console.WriteLine("------------------\n");
    }

    // Отмена макрокоманды - обр. порядок
    public void Undo()
    {
        Console.WriteLine("\n----------------");
        Console.WriteLine($"<<< ОТМЕНА МАКРОКОМАНДЫ: {_description}");
        Console.WriteLine("----------------\n");

        while (_executedCommands.Count > 0)
        {
            var command = _executedCommands.Pop();
            command.Undo();
        }

        Console.WriteLine("\n----------------");
        Console.WriteLine($"<<< МАКРОКОМАНДА '{_description}' ОТМЕНЕНА");
        Console.WriteLine("----------------\n");
    }

    public string Description => _description;

    // Метод для добавления команды в макрокоманду
    public void AddCommand(ICommand command)
    {
        _commands.Add(command);
    }
}