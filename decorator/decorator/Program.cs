
using HttpDecoratorSystem.Handlers;
using HttpDecoratorSystem.Interfaces;
using HttpDecoratorSystem.Models;
using HttpDecoratorSystem.Services;

namespace HttpDecoratorSystem
{
    class Program
    {
        static async Task Main(string[] args)
        {

            await RunDecoratorDemoAsync();

            Console.WriteLine("\nНажмите любую клавишу для выхода...");
            Console.ReadKey();
        }



        private static async Task RunDecoratorDemoAsync()
        {
            Console.WriteLine("обработка http запросов");
            Console.WriteLine("-----------------\n");

            // собираем цепочку - Logging  RateLimit  Auth  Caching  Handler
            var pipeline = BuildRequestPipeline();

            var requests = CreateTestRequests();

            // обрабатываем каждый запрос 
            foreach (var request in requests)
            {
                Console.WriteLine("--------------------\n");
                Console.WriteLine($"ЗАПРОС: {request.Method} {request.Url}");
                Console.WriteLine("--------------------\n");

                // запускаем обработку запроса через цепочку декораторов
                var response = await pipeline.HandleAsync(request);

                DisplayResponse(response);
            }
        }


        private static IHttpRequestHandler BuildRequestPipeline()
        {
            // базовый обработчик
            var rootHandler = new ApiEndpointHandler("UserProfile");

            // сервисы
            var logger = new ConsoleLogger();
            var tokenValidator = new TokenValidator();
            var cache = new InMemoryCache();
            var rateLimiter = new RateLimiter(maxRequests: 5, windowSize: TimeSpan.FromMinutes(1));

            //  сборка цепочки обёрток 
            return new RequestPipeline(rootHandler)
                .WithLogging(logger)          
                .WithRateLimit(rateLimiter)   
                .WithAuth(tokenValidator)     
                .WithCaching(cache, TimeSpan.FromMinutes(5))  
                .Build();
        }


        // тестовые запросы
        private static List<HttpRequest> CreateTestRequests()
        {
            return new List<HttpRequest>
            {
                // Валидный токен, первый запрос (кэш-мисс)
                new HttpRequest("GET", "/api/user/profile")
                {
                    AuthToken = "valid_token_123",
                    UserId = "user_42"
                },
                // Тот же запрос (должен попасть в кэш)
                new HttpRequest("GET", "/api/user/profile")
                {
                    AuthToken = "valid_token_123",
                    UserId = "user_42"
                },
                // Невалидный токен (должен получить 401)
                new HttpRequest("GET", "/api/user/profile")
                {
                    AuthToken = "invalid_token",
                    UserId = "anonymous"
                },
                // POST-запрос (не кэшируется)
                new HttpRequest("POST", "/api/user/update")
                {
                    AuthToken = "user_token_456",
                    UserId = "user_99",
                    Body = "{\"name\": \"New Name\"}"
                }
            };
        }


        // отображает результат обработки запроса
        private static void DisplayResponse(HttpResponse response)
        {
            Console.WriteLine("--------------------\n");
            Console.WriteLine("ОТВЕТ СЕРВЕРА:");
            Console.WriteLine("--------------------\n");
            Console.WriteLine($"Status: {response.StatusCode} {(response.IsSuccess ? "+" : "-")}");
            Console.WriteLine($"Время выполнения: {response.ExecutionTime.TotalMilliseconds:F2}ms");
            Console.WriteLine($"Кэш: {response.Headers.GetValueOrDefault("X-Cache", "N/A")}");
            Console.WriteLine($"Body: {response.Body}\n\n");
        }

    }
}
