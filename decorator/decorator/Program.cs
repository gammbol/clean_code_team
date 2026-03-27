// Main содержит минимум кода — только оркестрацию.
// Вся логика вынесена в отдельные методы для читаемости.

using HttpDecoratorSystem.Handlers;
using HttpDecoratorSystem.Interfaces;
using HttpDecoratorSystem.Models;
using HttpDecoratorSystem.Services;

namespace HttpDecoratorSystem
{
    class Program
    {
        // Точка входа в приложение
        static async Task Main(string[] args)
        {

            // Запускаем демонстрацию
            await RunDecoratorDemoAsync();

            Console.WriteLine("\nНажмите любую клавишу для выхода...");
            Console.ReadKey();
        }

        // Точка входа: запускает демонстрацию паттерна Декоратор
        private static async Task RunDecoratorDemoAsync()
        {
            Console.WriteLine("ОБРАБОТКА HTTP-ЗАПРОСОВ ЧЕРЕЗ ДЕКОРАТОРЫ");
            Console.WriteLine("-----------------\n");

            // Создаём конвейер обработки с декораторами 
            // Здесь мы собираем цепочку: Logging → RateLimit → Auth → Caching → Handler
            var pipeline = BuildRequestPipeline();

            // Создаём тестовые запросы 
            var requests = CreateTestRequests();

            // Обрабатываем каждый запрос 
            foreach (var request in requests)
            {
                Console.WriteLine("--------------------\n");
                Console.WriteLine($"ЗАПРОС: {request.Method} {request.Url}");
                Console.WriteLine("--------------------\n");

                // Запускаем обработку запроса через цепочку декораторов
                var response = await pipeline.HandleAsync(request);

                // Выводим результат
                DisplayResponse(response);
            }
        }


        // Строит конвейер обработки: собирает декораторы в цепочку
        private static IHttpRequestHandler BuildRequestPipeline()
        {
            // 1. Создаём базовый обработчик (ConcreteComponent)
            // Это ядро системы — содержит основную бизнес-логику
            var rootHandler = new ApiEndpointHandler("UserProfile");

            // 2. Создаём сервисы, которые будут использоваться декораторами
            var logger = new ConsoleLogger();
            var tokenValidator = new TokenValidator();
            var cache = new InMemoryCache();
            var rateLimiter = new RateLimiter(maxRequests: 5, windowSize: TimeSpan.FromMinutes(1));

            //  Сборка цепочки обёрток 
            //  Декораторы выполняются в порядке добавления:
            // 1. Logging (внешний) → 2. RateLimit → 3. Auth → 4. Caching → 5. Handler (ядро)
            // При возврате: Handler → Caching → Auth → RateLimit → Logging
            return new RequestPipeline(rootHandler)
                .WithLogging(logger)           // 1. Логирование (внешняя обёртка)
                .WithRateLimit(rateLimiter)    // 2. Rate Limit
                .WithAuth(tokenValidator)      // 3. Авторизация
                .WithCaching(cache, TimeSpan.FromMinutes(5))  // 4. Кэш (ближе к ядру)
                .Build();
        }

        // Создаёт тестовые запросы для демонстрации разных сценариев
        private static List<HttpRequest> CreateTestRequests()
        {
            return new List<HttpRequest>
            {
                // Запрос 1: Валидный токен, первый запрос (кэш-мисс)
                new HttpRequest("GET", "/api/user/profile")
                {
                    AuthToken = "valid_token_123",
                    UserId = "user_42"
                },
                // Запрос 2: Тот же запрос (должен попасть в кэш)
                new HttpRequest("GET", "/api/user/profile")
                {
                    AuthToken = "valid_token_123",
                    UserId = "user_42"
                },
                // Запрос 3: Невалидный токен (должен получить 401)
                new HttpRequest("GET", "/api/user/profile")
                {
                    AuthToken = "invalid_token",
                    UserId = "anonymous"
                },
                // Запрос 4: POST-запрос (не кэшируется)
                new HttpRequest("POST", "/api/user/update")
                {
                    AuthToken = "user_token_456",
                    UserId = "user_99",
                    Body = "{\"name\": \"New Name\"}"
                }
            };
        }

        // Отображает результат обработки запроса
        private static void DisplayResponse(HttpResponse response)
        {
            Console.WriteLine("--------------------\n");
            Console.WriteLine("ОТВЕТ СЕРВЕРА:");
            Console.WriteLine("--------------------\n");
            Console.WriteLine($"Status: {response.StatusCode} {(response.IsSuccess ? "+" : "-")}");
            Console.WriteLine($"Время выполнения: {response.ExecutionTime.TotalMilliseconds:F2}ms");
            Console.WriteLine($"Кэш: {response.Headers.GetValueOrDefault("X-Cache", "N/A")}");
            Console.WriteLine($"Body: {response.Body}");
        }

    }
}