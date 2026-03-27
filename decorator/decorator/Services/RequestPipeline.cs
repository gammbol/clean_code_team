// Builder для сборки цепочки декораторов
// Упрощает создание цепочки обработчиков.
// Вместо вложенных конструкторов используем fluent-интерфейс.

namespace HttpDecoratorSystem.Services
{
    using HttpDecoratorSystem.Interfaces;
    using HttpDecoratorSystem.Decorators;

    public class RequestPipeline
    {
        // Текущий обработчик в цепочке
        private IHttpRequestHandler _handler;

        // Конструктор: начинает цепочку с базового обработчика
        public RequestPipeline(IHttpRequestHandler rootHandler)
        {
            _handler = rootHandler;
        }


        // Добавляет декоратор логирования в цепочку
        public RequestPipeline WithLogging(ILogger logger)
        {
            // Оборачиваем текущий обработчик в LoggingDecorator
            _handler = new LoggingDecorator(_handler, logger);
            return this;  // Возвращаем this для fluent-интерфейса
        }


        // Добавляет декоратор авторизации в цепочку
        public RequestPipeline WithAuth(ITokenValidator validator, bool requireAuth = true)
        {
            _handler = new AuthDecorator(_handler, validator, requireAuth);
            return this;
        }


        // Добавляет декоратор кэширования в цепочку
        public RequestPipeline WithCaching(ICache cache, TimeSpan ttl, bool onlyForGet = true)
        {
            _handler = new CachingDecorator(_handler, cache, ttl, onlyForGet);
            return this;
        }


        // Добавляет декоратор ограничения частоты в цепочку
        public RequestPipeline WithRateLimit(IRateLimiter limiter)
        {
            _handler = new RateLimitDecorator(_handler, limiter);
            return this;
        }


        // Возвращает готовый обработчик со всеми декораторами
        public IHttpRequestHandler Build() => _handler;
    }
}