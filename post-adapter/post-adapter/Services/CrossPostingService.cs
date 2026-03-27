using CrossPostingSystem.Interfaces;
using CrossPostingSystem.Models;


namespace CrossPostingSystem.Services
{
    public class CrossPostingService
    {
        private readonly List<IPublisher> _publishers;

        public CrossPostingService()
        {
            _publishers = new List<IPublisher>();
        }

        public void AddPublisher(IPublisher publisher)
        {
            _publishers.Add(publisher);
        }

        public void RemovePublisher(string networkName)
        {
            var publisher = _publishers.FirstOrDefault(p => p.NetworkName == networkName);
            if (publisher != null)
                _publishers.Remove(publisher);
        }

        public List<PublicationResult> Publish(Post post)
        {
            var results = new List<PublicationResult>();

            Console.WriteLine("---  ---  ---  --- ---\n");
            Console.WriteLine($"НАЧАЛО КРОССПОСТИНГА");
            Console.WriteLine("---  ---  ---  --- ---\n");
            Console.WriteLine($"\nПост: {post.Content}");

            if (post.Tags.Count > 0)
                Console.WriteLine($"Теги: {string.Join(", ", post.Tags)}");
            Console.WriteLine($"\nПланируется публикация в {_publishers.Count} сетях:");
            foreach (var pub in _publishers)
                Console.WriteLine($"  - {pub.NetworkName}");

            foreach (var publisher in _publishers)
            {
                var result = new PublicationResult
                {
                    NetworkName = publisher.NetworkName,
                    PublishedAt = DateTime.Now
                };

                // Проверяем авторизацию
                if (!publisher.IsAuthorized())
                {
                    result.Success = false;
                    result.ErrorMessage = "Не авторизован";
                    results.Add(result);
                    continue;
                }

                // Публикуем
                bool success = publisher.Publish(post);

                result.Success = success;
                result.ExternalId = success ? Guid.NewGuid().ToString() : null;
                result.ErrorMessage = success ? null : "Ошибка публикации";

                results.Add(result);
            }

            // Выводим итоги
            Console.WriteLine("---  ---  ---  --- ---\n");
            Console.WriteLine("итоги публикации:");
            Console.WriteLine("---  ---  ---  --- ---\n");

            int successCount = results.Count(r => r.Success);
            foreach (var result in results)
                Console.WriteLine(result.ToString());

            Console.WriteLine($"\nВсего: {results.Count}, Успешно: {successCount}, Ошибок: {results.Count - successCount}");
            Console.WriteLine("---  ---  ---  --- ---\n");

            return results;
        }

        public void ShowConnectedNetworks()
        {
            Console.WriteLine("\nПодключенные сети:");
            foreach (var publisher in _publishers)
            {
                string status = publisher.IsAuthorized() ? "+" : "-";
                Console.WriteLine($"  {status} {publisher.NetworkName}");
            }
        }
    }
}