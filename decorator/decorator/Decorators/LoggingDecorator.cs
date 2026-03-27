// Конкретный декоратор
// Добавляет функциональность ЛОГИРОВАНИЯ запросов и ответов.
// Логирует: что пришло, сколько времени обрабатывалось, что ушло.

using HttpDecoratorSystem.Interfaces;
using HttpDecoratorSystem.Models;
using HttpDecoratorSystem.Services;
using System.Diagnostics;

namespace HttpDecoratorSystem.Decorators
{
    public class LoggingDecorator : RequestHandlerDecorator
    {
        // Сервис логирования (интерфейс, чтобы можно было подменить реализацию)
        private readonly ILogger _logger;

        // Конструктор: принимает следующий обработчик и логгер
        public LoggingDecorator(IHttpRequestHandler innerHandler, ILogger logger)
            : base(innerHandler)
        {
            _logger = logger;
        }


        /// Переопределяем метод обработки, добавляя логирование

        public override async Task<HttpResponse> HandleAsync(HttpRequest request, CancellationToken ct = default)
        {
            // Запускаем секундомер для замера времени выполнения
            var stopwatch = Stopwatch.StartNew();

            // Логирование до обработки 
            _logger.LogInfo($"[LOG] Запрос: {request.Method} {request.Url}");
            _logger.LogInfo($"[LOG] User: {request.UserId ?? "anonymous"}");
            _logger.LogInfo($"[LOG] Headers: {request.Headers.Count}");

            // Вызываем следующий обработчик в цепочке
            // Это может быть другой декоратор или базовый обработчик
            var response = await _innerHandler.HandleAsync(request, ct);

            // Останавливаем секундомер
            stopwatch.Stop();

            // Логирование после обработки 
            _logger.LogInfo($"[LOG] Ответ: {response.StatusCode} ({stopwatch.ElapsedMilliseconds}ms)");

            // Сохраняем время выполнения в ответ (для мониторинга)
            response.ExecutionTime = stopwatch.Elapsed;
            return response;
        }
    }
}