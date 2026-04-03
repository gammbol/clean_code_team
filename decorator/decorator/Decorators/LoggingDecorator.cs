// Конкретный декоратор
// логирование запросов и ответов

using HttpDecoratorSystem.Interfaces;
using HttpDecoratorSystem.Models;
using HttpDecoratorSystem.Services;
using System.Diagnostics;

namespace HttpDecoratorSystem.Decorators
{
    public class LoggingDecorator : RequestHandlerDecorator
    {
        private readonly ILogger _logger;

        public LoggingDecorator(IHttpRequestHandler innerHandler, ILogger logger)
            : base(innerHandler)
        {
            _logger = logger;
        }


        // + логирование
        public override async Task<HttpResponse> HandleAsync(HttpRequest request, CancellationToken ct = default)
        {
            // секундомер для замера времени выполнения
            var stopwatch = Stopwatch.StartNew();

            _logger.LogInfo($"(дек лог): Запрос {request.Method} {request.Url}");
            _logger.LogInfo($"(дек лог):  User {request.UserId ?? "anonymous"}");
            _logger.LogInfo($"(дек лог): Headers {request.Headers.Count}");

            var response = await _innerHandler.HandleAsync(request, ct);

            stopwatch.Stop();

            // логирование после обработки 
            _logger.LogInfo($"(дек лог): Ответ: {response.StatusCode} ({stopwatch.ElapsedMilliseconds}ms)");

            response.ExecutionTime = stopwatch.Elapsed;
            return response;
        }
    }
}
