// Модель HTTP-ответа

namespace HttpDecoratorSystem.Models
{
    public class HttpResponse
    {
        // 200 (OK), 401 (Unauthorized), 500 (Error) и т.д.
        public int StatusCode { get; set; }

        public string Body { get; set; }

        public Dictionary<string, string> Headers { get; set; }

        public TimeSpan ExecutionTime { get; set; }

        public bool IsSuccess => StatusCode >= 200 && StatusCode < 300;

        public HttpResponse()
        {
            StatusCode = 200;  // По умолчанию успешный ответ
            Headers = new Dictionary<string, string>();
        }

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
