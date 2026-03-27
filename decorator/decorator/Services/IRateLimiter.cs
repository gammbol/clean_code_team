// Интерфейс Rate Limiter и его реализация
// Ограничивает количество запросов от одного пользователя за определённое время

namespace HttpDecoratorSystem.Services
{
    public interface IRateLimiter
    {
        Task<bool> CheckAsync(string userId, string endpoint, CancellationToken ct = default);
    }

    // Реализация: скользящее окно (sliding window)
    public class RateLimiter : IRateLimiter
    {
        private readonly int _maxRequests;  // Максимум запросов
        private readonly TimeSpan _windowSize;  // Временное окно
        private readonly Dictionary<string, List<DateTime>> _requests;  // История запросов

        // Конструктор: настройки лимита
        public RateLimiter(int maxRequests = 10, TimeSpan? windowSize = null)
        {
            _maxRequests = maxRequests;
            _windowSize = windowSize ?? TimeSpan.FromMinutes(1);  // По умолчанию 1 минута
            _requests = new Dictionary<string, List<DateTime>>();
        }

        // Проверяет: можно ли сделать запрос (не превышен ли лимит)
        public Task<bool> CheckAsync(string userId, string endpoint, CancellationToken ct = default)
        {
            // Уникальный ключ для каждого пользователя+эндпоинта
            var key = $"{userId}:{endpoint}";
            var now = DateTime.Now;
            var windowStart = now - _windowSize;  // Начало временного окна

            // Если нет истории запросов — создаём
            if (!_requests.ContainsKey(key))
                _requests[key] = new List<DateTime>();

            // Удаляем старые запросы за пределами окна (скользящее окно)
            _requests[key] = _requests[key]
                .Where(t => t > windowStart)
                .ToList();

            // Проверяем: не превышен ли лимит
            if (_requests[key].Count >= _maxRequests)
                return Task.FromResult(false);  // Лимит превышен

            // Добавляем текущий запрос в историю
            _requests[key].Add(now);
            return Task.FromResult(true);  // Можно выполнять запрос
        }
    }
}