// Модель HTTP-запроса
// Хранит все данные, которые приходят от клиента

namespace HttpDecoratorSystem.Models
{
    public class HttpRequest
    {
        // Уникальный идентификатор запроса (для логирования и отслеживания)
        public string Id { get; set; }

        // HTTP-метод: GET, POST, PUT, DELETE и т.д.
        public string Method { get; set; }

        // URL адрес, куда направлен запрос
        public string Url { get; set; }

        // Токен авторизации (если есть)
        public string AuthToken { get; set; }

        // ID пользователя, сделавшего запрос
        public string UserId { get; set; }

        // Заголовки запроса (Content-Type, User-Agent и др.)
        public Dictionary<string, string> Headers { get; set; }

        // Тело запроса (для POST/PUT запросов)
        public string Body { get; set; }

        // Время создания запроса
        public DateTime CreatedAt { get; set; }

        // Конструктор: создаёт новый запрос с методом и URL
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
                Headers[key] = value;  // Добавляем или обновляем заголовок
        }
    }
}