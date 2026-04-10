//Конкретный декоратор
// кеширование ответов

namespace HttpDecoratorSystem.Decorators
{
    using HttpDecoratorSystem.Interfaces;
    using HttpDecoratorSystem.Models;
    using HttpDecoratorSystem.Services;

    public class CachingDecorator : RequestHandlerDecorator
    {
        private readonly ICache _cache;

        private readonly TimeSpan _ttl;

        // кэшировать только GET-запросы
        private readonly bool _onlyForGet;

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

        // + кэширование
        public override async Task<HttpResponse> HandleAsync(HttpRequest request, CancellationToken ct = default)
        {
       
            if (_onlyForGet && request.Method.ToUpper() != "GET")
            {
                Console.WriteLine("(дек кэша): Пропуск кэша (не GET запрос)");
                return await _innerHandler.HandleAsync(request, ct);
            }

            // уникальный ключ кэша на основе запроса
            string cacheKey = GenerateCacheKey(request);

            // пробуем получить ответ из кэша
            var cachedResponse = await _cache.GetAsync(cacheKey, ct);

            // Если найдено - возвращаем сразу
            if (cachedResponse != null)
            {
                Console.WriteLine($"(дек кэша): Найдено в кэше: {cacheKey}");
                cachedResponse.Headers["Х-Cache"] = "HIT";  // Помечаем, что это из кэша
                return cachedResponse;
            }

            // запроса нет в кэше
            Console.WriteLine($"(дек кэша): Кэш-мисс, обработка запроса...");

            // вызываем следующий обработчик в цепочке
            var response = await _innerHandler.HandleAsync(request, ct);

            if (response.IsSuccess)
            {
                await _cache.SetAsync(cacheKey, response, _ttl, ct);
                response.Headers["Х-Cache"] = "MISS";  
                Console.WriteLine($"(дек кэша): Сохранено в кэш: {cacheKey}");
            }

            return response;
        }

        private string GenerateCacheKey(HttpRequest request)
        {
            return $"cache:{request.Method}:{request.Url}:{request.UserId}";
        }
    }
}
