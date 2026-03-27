using CrossPostingSystem.Interfaces;
using CrossPostingSystem.Models;
using CrossPostingSystem.ThirdParty;

namespace CrossPostingSystem.Adapters
{
    public class TelegramAdapter : IPublisher
    {
        private readonly TelegramBotClient _telegramClient;
        private readonly long _channelId;

        public string NetworkName => "Telegram";

        public TelegramAdapter(TelegramBotClient telegramClient, long channelId)
        {
            _telegramClient = telegramClient;
            _channelId = channelId;
        }

        public bool IsAuthorized()
        {
            return _telegramClient.TestConnection();
        }

        public bool Publish(Post post)
        {
            try
            {
                Console.WriteLine($"\n Публикация в {NetworkName} ");

                // Формируем HTML-разметку для Telegram
                string htmlContent = $"<b>{post.Content}</b>";

                if (post.Tags.Count > 0)
                    htmlContent += "\n\n" + string.Join(" ", post.Tags.Select(t => $"#{t}"));

                htmlContent += $"\n\n<i>Опубликовано: {post.CreatedAt:dd.MM.yyyy HH:mm}</i>";

                // Если есть фото, отправляем как фото с подписью
                if (!string.IsNullOrEmpty(post.ImageUrl))
                {
                    var response = _telegramClient.SendPhoto(post.ImageUrl, htmlContent);
                    if (response.Success)
                    {
                        Console.WriteLine($"{NetworkName}: Фото опубликовано (ID: {response.MessageId})");
                        return true;
                    }
                }
                else
                {
                    // Отправляем как текстовое сообщение
                    var response = _telegramClient.SendMessage(htmlContent);
                    if (response.Success)
                    {
                        Console.WriteLine($"{NetworkName}: Сообщение опубликовано (ID: {response.MessageId})");
                        return true;
                    }
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