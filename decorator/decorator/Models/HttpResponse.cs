// Модель HTTP-ответа
// Хранит результат обработки запроса

namespace HttpDecoratorSystem.Models
{
    public class HttpResponse
    {
        // HTTP-статус код: 200 (OK), 401 (Unauthorized), 500 (Error) и т.д.
        public int StatusCode { get; set; }

        // Тело ответа (данные или сообщение об ошибке)
        public string Body { get; set; }

        // Заголовки ответа (например, X-Cache: HIT/MISS)
        public Dictionary<string, string> Headers { get; set; }

        // Время выполнения запроса (для мониторинга производительности)
        public TimeSpan ExecutionTime { get; set; }

        // Вычисляемое свойство: true если статус код 2xx (успех)
        public bool IsSuccess => StatusCode >= 200 && StatusCode < 300;

        // Конструктор по умолчанию
        public HttpResponse()
        {
            StatusCode = 200;  // По умолчанию успешный ответ
            Headers = new Dictionary<string, string>();
        }

        // Статические фабричные методы для создания стандартных ответов
        // Это упрощает создание ответов и делает код чище

        public static HttpResponse Ok(string body) => new()
        {
            StatusCode = 200,
            Body = body
        };

        public static HttpResponse Unauthorized(string message) => new()
        {
            StatusCode = 401,  // 401 = нет авторизации
            Body = message
        };

        public static HttpResponse Forbidden(string message) => new()
        {
            StatusCode = 403,  // 403 = доступ запрещён
            Body = message
        };

        public static HttpResponse Error(string message) => new()
        {
            StatusCode = 500,  // 500 = внутренняя ошибка сервера
            Body = message
        };
    }
}