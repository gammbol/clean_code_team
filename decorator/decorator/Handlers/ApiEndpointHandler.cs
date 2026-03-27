// Конкретный компонент
// Это БАЗОВЫЙ обработчик — он содержит основную бизнес-логику.
// Все декораторы будут оборачивать этот обработчик и добавлять 
// дополнительную функциональность ДО или ПОСЛЕ его работы.

using HttpDecoratorSystem.Interfaces;
using HttpDecoratorSystem.Models; 
using System.Text.Json;


namespace HttpDecoratorSystem.Handlers
{
    public class ApiEndpointHandler : IHttpRequestHandler
    {
        // Имя эндпоинта (для логирования)
        private readonly string _endpointName;

        // Конструктор: принимает имя эндпоинта
        public ApiEndpointHandler(string endpointName = "Default")
        {
            _endpointName = endpointName;
        }

        // Основная бизнес-логика обработки запроса
        public async Task<HttpResponse> HandleAsync(HttpRequest request, CancellationToken ct = default)
        {
            Console.WriteLine($"[{_endpointName}] Обработка бизнес-логики...");

            // Эмуляция работы с базой данных (задержка 100мс)
            // В реальном проекте здесь был бы запрос к БД
            await Task.Delay(100, ct);

            // Формируем ответные данные
            var responseData = new
            {
                endpoint = _endpointName,
                userId = request.UserId,
                timestamp = DateTime.Now,
                message = $"Данные успешно получены из {_endpointName}"
            };

            Console.WriteLine($"[{_endpointName}] Бизнес-логика выполнена");

            // Сериализуем объект в JSON и возвращаем успешный ответ
            return HttpResponse.Ok(JsonSerializer.Serialize(responseData));
        }
    }
}