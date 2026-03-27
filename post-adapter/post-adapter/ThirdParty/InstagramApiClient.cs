namespace CrossPostingSystem.ThirdParty
{
    // Эмуляция Instagram Graph API
    public class InstagramApiClient
    {
        private readonly string _accessToken;
        private readonly string _igUserId;

        public InstagramApiClient(string accessToken, string igUserId)
        {
            _accessToken = accessToken;
            _igUserId = igUserId;
        }

        // Instagram требует сначала создать медиа-контейнер
        public InstagramContainerResponse CreateMediaContainer(string imageUrl, string caption)
        {
            Console.WriteLine($"[Instagram API] Создание медиа-контейнера для {imageUrl}");
            Console.WriteLine($"[Instagram API] Подпись: {caption}");

            System.Threading.Thread.Sleep(400);

            return new InstagramContainerResponse
            {
                ContainerId = $"container_{Guid.NewGuid().ToString().Substring(0, 8)}",
                Success = true
            };
        }

        // Затем опубликовать контейнер
        public InstagramPublishResponse PublishContainer(string containerId)
        {
            Console.WriteLine($"[Instagram API] Публикация контейнера {containerId}");

            System.Threading.Thread.Sleep(350);

            return new InstagramPublishResponse
            {
                Success = true,
                PublishedMediaId = $"media_{new Random().Next(100000000, 999999999)}"
            };
        }

        public bool ValidateAccessToken()
        {
            Console.WriteLine("[Instagram API] Валидация access token...");
            return !string.IsNullOrEmpty(_accessToken) && _accessToken.Length > 20;
        }
    }

    public class InstagramContainerResponse
    {
        public string? ContainerId { get; set; }
        public bool Success { get; set; }
        public string? Error { get; set; }
    }

    public class InstagramPublishResponse
    {
        public string? PublishedMediaId { get; set; }
        public bool Success { get; set; }
        public string? Error { get; set; }
    }
}