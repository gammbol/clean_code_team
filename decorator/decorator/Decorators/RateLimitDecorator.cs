// Конкретный декоратор
// огр частоты запросов 

namespace HttpDecoratorSystem.Decorators
{
    using HttpDecoratorSystem.Interfaces;
    using HttpDecoratorSystem.Models;
    using HttpDecoratorSystem.Services;

    public class RateLimitDecorator : RequestHandlerDecorator
    {
        // сервис ограничения частоты запросов
        private readonly IRateLimiter _rateLimiter;

        public RateLimitDecorator(IHttpRequestHandler innerHandler, IRateLimiter rateLimiter)
            : base(innerHandler)
        {
            _rateLimiter = rateLimiter;
        }

        // + проверка лимита
        public override async Task<HttpResponse> HandleAsync(HttpRequest request, CancellationToken ct = default)
        {
            // не превысил ли пользователь лимит запросов
            var isAllowed = await _rateLimiter.CheckAsync(request.UserId, request.Url, ct);

            if (!isAllowed)
            {
                Console.WriteLine($"(дек RL): Превышен лимит для {request.UserId}");
                return HttpResponse.Forbidden("Превышен лимит запросов. Попробуйте позже.");
            }

            Console.WriteLine($"(дек RL): Лимит не превышен");

            return await _innerHandler.HandleAsync(request, ct);
        }
    }
}
