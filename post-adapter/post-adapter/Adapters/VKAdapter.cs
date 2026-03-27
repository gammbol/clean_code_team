using CrossPostingSystem.Interfaces;
using CrossPostingSystem.Models;
using CrossPostingSystem.ThirdParty;



namespace CrossPostingSystem.Adapters
{
    public class VKAdapter : IPublisher
    {
        private readonly VKApiClient _vkClient;
        private readonly long _groupId;

        public string NetworkName => "ВКонтакте";

        public VKAdapter(VKApiClient vkClient, long groupId)
        {
            _vkClient = vkClient;
            _groupId = groupId;
        }

        public bool IsAuthorized()
        {
            return _vkClient.ValidateToken();
        }

        public bool Publish(Post post)
        {
            try
            {
                Console.WriteLine($"\n Публикация в {NetworkName} ");

                // Формируем текст с тегами
                string fullText = post.Content;
                if (post.Tags.Count > 0)
                    fullText += "\n\n" + string.Join(" ", post.Tags.Select(t => $"#{t}"));

                // Если есть фото, загружаем и прикрепляем
                string attachment = null;
                if (!string.IsNullOrEmpty(post.ImageUrl))
                {
                    var photoResponse = _vkClient.UploadPhoto(post.ImageUrl);
                    if (photoResponse.Success)
                        attachment = photoResponse.PhotoId;
                }

                // Публикуем на стене
                var response = _vkClient.PostToWall(fullText, attachment);

                if (response.Success)
                {
                    Console.WriteLine($" {NetworkName}: Пост опубликован (ID: {response.PostId})");
                    return true;
                }

                Console.WriteLine($" {NetworkName}: Ошибка публикации");
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($" {NetworkName}: Исключение - {ex.Message}");
                return false;
            }
        }
    }
}