// Интерфейс логгера и его консольная реализация
// Вынесен в отдельный сервис для соблюдения DIP (Dependency Inversion Principle)

namespace HttpDecoratorSystem.Services
{
    public interface ILogger
    {
        void LogInfo(string message);
        void LogWarning(string message);
        void LogError(string message);
    }

    // Консольная реализация логгера
    public class ConsoleLogger : ILogger
    {
        public void LogInfo(string message)
        {
            Console.WriteLine($"[INFO] {DateTime.Now:HH:mm:ss.fff} {message}");
        }

        public void LogWarning(string message)
        {
            // Жёлтый цвет для предупреждений
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"[WARN] {DateTime.Now:HH:mm:ss.fff} {message}");
            Console.ResetColor();
        }

        public void LogError(string message)
        {
            // Красный цвет для ошибок
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"[ERROR] {DateTime.Now:HH:mm:ss.fff} {message}");
            Console.ResetColor();
        }
    }
}