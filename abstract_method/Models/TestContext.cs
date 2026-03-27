namespace method_test.Models
{
    // Модель данных: контекст выполнения теста.
    // Передаётся в метод Execute() и содержит настройки окружения (таймауты, строки подключения).
    // Позволяет изолировать конфигурацию от логики теста.
    public class TestContext
    {
        public string Environment { get; set; } = "Local";
        public int TimeoutMs { get; set; } = 5000;
        public string ConnectionString { get; set; } = "";
    }
}