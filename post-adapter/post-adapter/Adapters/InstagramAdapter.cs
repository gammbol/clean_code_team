using CrossPostingSystem.Interfaces;
using CrossPostingSystem.Models;
using CrossPostingSystem.ThirdParty;

namespace CrossPostingSystem.Adapters
{
    public class InstagramAdapter : IPublisher
    {
        private readonly InstagramApiClient _instagramClient;
        private readonly string _igUserId;

        public string NetworkName => "Instagram";

        public InstagramAdapter(InstagramApiClient instagramClient, string igUserId)
        {
            _instagramClient = instagramClient;
            _igUserId = igUserId;
        }

        public bool IsAuthorized()
        {
            return _instagramClient.ValidateAccessToken();
        }

        public bool Publish(Post post)
        {
            try
            {
                Console.WriteLine($"\n Публикация в {NetworkName} ");

                // Instagram требует фото для публикации
                if (string.IsNullOrEmpty(post.ImageUrl))
                {
                    Console.WriteLine($"{NetworkName}: Требуется изображение для публикации");
                    return false;
                }

                // Формируем подпись с тегами
                string caption = post.Content;
                if (post.Tags.Count > 0)
                    caption += "\n\n" + string.Join(" ", post.Tags);

                // Шаг 1: Создаем медиа-контейнер
                var containerResponse = _instagramClient.CreateMediaContainer(post.ImageUrl, caption);

                if (!containerResponse.Success)
                {
                    Console.WriteLine($"{NetworkName}: Ошибка создания контейнера");
                    return false;
                }

                // Шаг 2: Публикуем контейнер
                var publishResponse = _instagramClient.PublishContainer(containerResponse.ContainerId);

                if (publishResponse.Success)
                {
                    Console.WriteLine($"{NetworkName}: Опубликовано (Media ID: {publishResponse.PublishedMediaId})");
                    return true;
                }

                Console.WriteLine($"{NetworkName}: Ошибка публикации");
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{NetworkName}: Исключение - {ex.Message}");
                return false;
            }
        }
    }
}