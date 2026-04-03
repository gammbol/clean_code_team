// Базовый декоратор

using HttpDecoratorSystem.Interfaces;
using HttpDecoratorSystem.Models;

namespace HttpDecoratorSystem.Decorators
{
    public abstract class RequestHandlerDecorator : IHttpRequestHandler
    {
        protected readonly IHttpRequestHandler _innerHandler;

        protected RequestHandlerDecorator(IHttpRequestHandler innerHandler)
        {
            _innerHandler = innerHandler;
        }

        public virtual Task<HttpResponse> HandleAsync(HttpRequest request, CancellationToken ct = default)
        {
            return _innerHandler.HandleAsync(request, ct);
        }
    }
}
