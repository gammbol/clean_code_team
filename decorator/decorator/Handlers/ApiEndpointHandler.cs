// Конкретный компонент

using HttpDecoratorSystem.Interfaces;
using HttpDecoratorSystem.Models; 
using System.Text.Json;


namespace HttpDecoratorSystem.Handlers
{
    public class ApiEndpointHandler : IHttpRequestHandler
    {
        private readonly string _endpointName;

        public ApiEndpointHandler(string endpointName = "Default")
        {
            _endpointName = endpointName;
        }

        public async Task<HttpResponse> HandleAsync(HttpRequest request, CancellationToken ct = default)
        {
            Console.WriteLine($"({_endpointName}) Обработка бизнес-логики...");

            // эмуляция работы с бд
            await Task.Delay(100, ct);

            // ответные данные
            var responseData = new
            {
                endpoint = _endpointName,
                userId = request.UserId,
                timestamp = DateTime.Now,
                message = $"Данные успешно получены из {_endpointName}"
            };

            Console.WriteLine($"({_endpointName}) бизнес-логика выполнена");

            // сериализуем объект в JSON и возвращаем успешный ответ
            return HttpResponse.Ok(JsonSerializer.Serialize(responseData));
        }
    }
}
