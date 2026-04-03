// Интерфейс кэша и его in-memory реализация
// В реальном проекте здесь мог бы быть Redis или Memcached

using HttpDecoratorSystem.Models;

namespace HttpDecoratorSystem.Services
{
    public interface ICache
    {
        Task<HttpResponse> GetAsync(string key, CancellationToken ct = default);
        Task SetAsync(string key, HttpResponse response, TimeSpan ttl, CancellationToken ct = default);
        Task RemoveAsync(string key, CancellationToken ct = default);
    }

    // Простая реализация кэша в памяти
    public class InMemoryCache : ICache
    {
        // Хранилище кэша: ключ, запись с временем истечения
        private readonly Dictionary<string, CacheEntry> _cache;

        public InMemoryCache()
        {
            _cache = new Dictionary<string, CacheEntry>();
        }

        // Получить значение из кэша по ключу
        public Task<HttpResponse> GetAsync(string key, CancellationToken ct = default)
        {
            if (_cache.TryGetValue(key, out var entry))
            {
                // не истёк ли срок жизни кэша
                if (entry.ExpiresAt > DateTime.Now)
                    return Task.FromResult(entry.Response);

                // Истёк — удаляем 
                _cache.Remove(key);
            }

            // не найдено в кэше
            return Task.FromResult<HttpResponse>(null);
        }

        // Сохранить значение в кэш с указанным TTL
        public Task SetAsync(string key, HttpResponse response, TimeSpan ttl, CancellationToken ct = default)
        {
            _cache[key] = new CacheEntry
            {
                Response = response,
                ExpiresAt = DateTime.Now.Add(ttl)  // Время истечения = сейчас + TTL
            };

            Console.WriteLine($"(кэш) Установлен ключ: {key}   TTL: {ttl.TotalSeconds}s");
            return Task.CompletedTask;
        }

        // Удалить значение из кэша
        public Task RemoveAsync(string key, CancellationToken ct = default)
        {
            _cache.Remove(key);
            return Task.CompletedTask;
        }

        // Внутренний класс для хранения записи кэша
        private class CacheEntry
        {
            public HttpResponse Response { get; set; }
            public DateTime ExpiresAt { get; set; }  // Когда кэш устареет
        }
    }
}
