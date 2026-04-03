// Интерфейс логгера и его консольная реализация

namespace HttpDecoratorSystem.Services
{
    public interface ILogger
    {
        void LogInfo(string message);
        void LogWarning(string message);
        void LogError(string message);
    }

    public class ConsoleLogger : ILogger
    {
        public void LogInfo(string message)
        {
            Console.WriteLine($"(лог INFO) {DateTime.Now:HH:mm:ss.fff} {message}");
        }

        public void LogWarning(string message)
        {
            // Жёлтый цвет для предупреждений
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"(лог WARNING) {DateTime.Now:HH:mm:ss.fff} {message}");
            Console.ResetColor();
        }

        public void LogError(string message)
        {
            // Красный цвет для ошибок
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"(лог ERROR) {DateTime.Now:HH:mm:ss.fff} {message}");
            Console.ResetColor();
        }
    }
}
