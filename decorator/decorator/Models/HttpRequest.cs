// Модель HTTP-запроса
// Хранит все данные которые приходят от клиента

namespace HttpDecoratorSystem.Models
{
    public class HttpRequest
    {
        public string Id { get; set; }

        // HTTP-метод: GET, POST, PUT, DELETE и т.д.
        public string Method { get; set; }

        public string Url { get; set; }

        public string AuthToken { get; set; }

        public string UserId { get; set; }

        // Заголовки запроса (Content-Type, User-Agent и др.)
        public Dictionary<string, string> Headers { get; set; }

        public string Body { get; set; }

        public DateTime CreatedAt { get; set; }

        public HttpRequest(string method, string url)
        {
            Id = Guid.NewGuid().ToString();  // Генерируем уникальный ID
            Method = method;
            Url = url;
            Headers = new Dictionary<string, string>();  // Инициализируем коллекцию заголовков
            CreatedAt = DateTime.Now;
        }

        // Метод для добавления заголовка в запрос
        public void AddHeader(string key, string value)
        {
            if (!string.IsNullOrWhiteSpace(key))
                Headers[key] = value; 
        }
    }
}
