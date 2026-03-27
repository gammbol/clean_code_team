//Конкретный декоратор
// Добавляет функциональность КЭШИРОВАНИЯ ответов.
// Если запрос уже был — возвращает ответ из кэша, не вызывая базовый обработчик.

namespace HttpDecoratorSystem.Decorators
{
    using HttpDecoratorSystem.Interfaces;
    using HttpDecoratorSystem.Models;
    using HttpDecoratorSystem.Services;

    public class CachingDecorator : RequestHandlerDecorator
    {
        // Сервис кэширования (хранит ответы)
        private readonly ICache _cache;

        // Время жизни кэша (TTL - Time To Live)
        private readonly TimeSpan _ttl;

        // Флаг: кэшировать только GET-запросы (POST/PUT обычно не кэшируют)
        private readonly bool _onlyForGet;

        // Конструктор
        public CachingDecorator(IHttpRequestHandler innerHandler,
            ICache cache,
            TimeSpan ttl,
            bool onlyForGet = true)
            : base(innerHandler)
        {
            _cache = cache;
            _ttl = ttl;
            _onlyForGet = onlyForGet;
        }

        // Переопределяем метод обработки, добавляя кэширование
        public override async Task<HttpResponse> HandleAsync(HttpRequest request, CancellationToken ct = default)
        {
            //Проверка кэша

            // Если не GET-запрос — пропускаем кэш (POST/PUT меняют данные)
            if (_onlyForGet && request.Method.ToUpper() != "GET")
            {
                Console.WriteLine("[CACHE] Пропуск кэша (не GET запрос)");
                return await _innerHandler.HandleAsync(request, ct);
            }

            // Генерируем уникальный ключ кэша на основе запроса
            string cacheKey = GenerateCacheKey(request);

            // Пробуем получить ответ из кэша
            var cachedResponse = await _cache.GetAsync(cacheKey, ct);

            // Если найдено в кэше — возвращаем сразу, не вызывая обработчик
            if (cachedResponse != null)
            {
                Console.WriteLine($"[CACHE] Найдено в кэше: {cacheKey}");
                cachedResponse.Headers["Х-Cache"] = "HIT";  // Помечаем, что это из кэша
                return cachedResponse;
            }

            // Кэш-мисс: запроса нет в кэше, нужно обработать
            Console.WriteLine($"[CACHE] Кэш-мисс, обработка запроса...");

            // Вызываем следующий обработчик в цепочке
            var response = await _innerHandler.HandleAsync(request, ct);

            // Если ответ успешный — сохраняем в кэш для будущих запросов
            if (response.IsSuccess)
            {
                await _cache.SetAsync(cacheKey, response, _ttl, ct);
                response.Headers["Х-Cache"] = "MISS";  // Помечаем, что это новый ответ
                Console.WriteLine($"[CACHE] Сохранено в кэш: {cacheKey}");
            }

            return response;
        }

        // Генерация уникального ключа для кэша
        private string GenerateCacheKey(HttpRequest request)
        {
            // Ключ зависит от метода, URL и пользователя
            // Это нужно, чтобы разные пользователи не получали чужие данные
            return $"cache:{request.Method}:{request.Url}:{request.UserId}";
        }
    }
}