// Компонент
// Общий интерфейс для всех обработчиков и декораторов
using HttpDecoratorSystem.Models;

namespace HttpDecoratorSystem.Interfaces
{
    public interface IHttpRequestHandler
    {
        Task<HttpResponse> HandleAsync(HttpRequest request, CancellationToken ct = default);
    }
}