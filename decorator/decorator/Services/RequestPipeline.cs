// для сборки цепочки декораторов


namespace HttpDecoratorSystem.Services
{
    using HttpDecoratorSystem.Interfaces;
    using HttpDecoratorSystem.Decorators;

    public class RequestPipeline
    {
        // текущий обработчик в цепочке
        private IHttpRequestHandler _handler;

        // начинает цепочку с базового обработчика
        public RequestPipeline(IHttpRequestHandler rootHandler)
        {
            _handler = rootHandler;
        }


        // добавляем декоратор логирования в цепочку
        public RequestPipeline WithLogging(ILogger logger)
        {
            _handler = new LoggingDecorator(_handler, logger);
            return this;  
        }


        // добавляем декоратор авторизации в цепочку
        public RequestPipeline WithAuth(ITokenValidator validator, bool requireAuth = true)
        {
            _handler = new AuthDecorator(_handler, validator, requireAuth);
            return this;
        }


        // добавляем декоратор кэширования в цепочку
        public RequestPipeline WithCaching(ICache cache, TimeSpan ttl, bool onlyForGet = true)
        {
            _handler = new CachingDecorator(_handler, cache, ttl, onlyForGet);
            return this;
        }


        // добавляем декоратор ограничения частоты в цепочку
        public RequestPipeline WithRateLimit(IRateLimiter limiter)
        {
            _handler = new RateLimitDecorator(_handler, limiter);
            return this;
        }


        // возвращает готовый обработчик со всеми декораторами
        public IHttpRequestHandler Build() => _handler;
    }
}
