// Конкретный декоратор
// Добавляет функциональность ОГРАНИЧЕНИЯ ЧАСТОТЫ ЗАПРОСОВ (Rate Limiting).
// Защищает от DDoS и злоупотреблений API.

namespace HttpDecoratorSystem.Decorators
{
    using HttpDecoratorSystem.Interfaces;
    using HttpDecoratorSystem.Models;
    using HttpDecoratorSystem.Services;

    public class RateLimitDecorator : RequestHandlerDecorator
    {
        // Сервис ограничения частоты запросов
        private readonly IRateLimiter _rateLimiter;

        // Конструктор
        public RateLimitDecorator(IHttpRequestHandler innerHandler, IRateLimiter rateLimiter)
            : base(innerHandler)
        {
            _rateLimiter = rateLimiter;
        }

        // Переопределяем метод обработки, добавляя проверку лимита
        public override async Task<HttpResponse> HandleAsync(HttpRequest request, CancellationToken ct = default)
        {
            // Проверка лимита 

            // Проверяем: не превысил ли пользователь лимит запросов
            var isAllowed = await _rateLimiter.CheckAsync(request.UserId, request.Url, ct);

            // Если лимит превышен — возвращаем 403 Forbidden
            if (!isAllowed)
            {
                Console.WriteLine($"[RATE LIMIT] Превышен лимит для {request.UserId}");
                return HttpResponse.Forbidden("Превышен лимит запросов. Попробуйте позже.");
            }

            // Лимит не превышен — передаём запрос дальше
            Console.WriteLine($"[RATE LIMIT] Лимит не превышен");

            return await _innerHandler.HandleAsync(request, ct);
        }
    }
}