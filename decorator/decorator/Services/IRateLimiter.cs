// Интерфейс Rate Limiter и его реализация

namespace HttpDecoratorSystem.Services
{
    public interface IRateLimiter
    {
        Task<bool> CheckAsync(string userId, string endpoint, CancellationToken ct = default);
    }

    // скользящее окно (sliding window)
    public class RateLimiter : IRateLimiter
    {
        private readonly int _maxRequests; 
        private readonly TimeSpan _windowSize;  // временное окно
        private readonly Dictionary<string, List<DateTime>> _requests;  // история запросов

        // настройки лимита
        public RateLimiter(int maxRequests = 10, TimeSpan? windowSize = null)
        {
            _maxRequests = maxRequests;
            _windowSize = windowSize ?? TimeSpan.FromMinutes(1); 
            _requests = new Dictionary<string, List<DateTime>>();
        }

        // можно ли сделать запрос (не превышен ли лимит)
        public Task<bool> CheckAsync(string userId, string endpoint, CancellationToken ct = default)
        {
            // уникальный ключ для каждого пользователя+эндпоинта
            var key = $"{userId}:{endpoint}";
            var now = DateTime.Now;
            var windowStart = now - _windowSize;  

            // нет истории запросов — создаём
            if (!_requests.ContainsKey(key))
                _requests[key] = new List<DateTime>();

            // удаляем старые запросы за пределами окна 
            _requests[key] = _requests[key]
                .Where(t => t > windowStart)
                .ToList();

            // не превышен ли лимит
            if (_requests[key].Count >= _maxRequests)
                return Task.FromResult(false);  

            // добавляем текущий запрос в историю
            _requests[key].Add(now);
            return Task.FromResult(true);  
        }
    }
}
