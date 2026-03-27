// Базовый декоратор
// Это абстрактный класс-обёртка.
// Он хранит ссылку на следующий обработчик в цепочке и делегирует ему вызов.
// Все конкретные декораторы (Logging, Auth, Caching) наследуются от этого класса.

using HttpDecoratorSystem.Interfaces;
using HttpDecoratorSystem.Models;

namespace HttpDecoratorSystem.Decorators
{
    public abstract class RequestHandlerDecorator : IHttpRequestHandler
    {
        // Ссылка на следующий обработчик в цепочке
        // Это может быть другой декоратор или базовый ApiEndpointHandler
        protected readonly IHttpRequestHandler _innerHandler;

        // Конструктор: принимает следующий обработчик и сохраняет его
        protected RequestHandlerDecorator(IHttpRequestHandler innerHandler)
        {
            _innerHandler = innerHandler;
        }

        // По умолчанию просто делегирует вызов следующему обработчику.
        // Конкретные декораторы переопределяют этот метод, добавляя 
        // свою логику ДО или ПОСЛЕ вызова _innerHandler.HandleAsync()
        public virtual Task<HttpResponse> HandleAsync(HttpRequest request, CancellationToken ct = default)
        {
            return _innerHandler.HandleAsync(request, ct);
        }
    }
}