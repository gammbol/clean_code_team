namespace CrossPostingSystem.ThirdParty
{
    // Эмуляция стороннего SDK ВКонтакте
    public class VKApiClient
    {
        private readonly string _accessToken;
        private readonly long _groupId;

        public VKApiClient(string accessToken, long groupId)
        {
            _accessToken = accessToken;
            _groupId = groupId;
        }

        // Метод VK API - публикует на стене
        public VKResponse PostToWall(string message, string attachment = null)
        {
            Console.WriteLine($"[VK SDK] Вызов API.wall.post для группы {_groupId}");
            Console.WriteLine($"[VK SDK] Сообщение: {message}");

            // Симуляция задержки сети
            System.Threading.Thread.Sleep(300);

            return new VKResponse
            {
                PostId = new Random().Next(100000, 999999),
                Success = true
            };
        }

        // Метод VK API - загрузка фото
        public VKPhotoResponse UploadPhoto(string imageUrl)
        {
            Console.WriteLine($"[VK SDK] Загрузка фото: {imageUrl}");
            System.Threading.Thread.Sleep(200);

            return new VKPhotoResponse
            {
                PhotoId = $"photo{new Random().Next(1000000, 9999999)}",
                Success = true
            };
        }

        public bool ValidateToken()
        {
            Console.WriteLine("[VK SDK] Валидация токена...");
            return !string.IsNullOrEmpty(_accessToken) && _accessToken.Length > 10;
        }
    }

    public class VKResponse
    {
        public int PostId { get; set; }
        public bool Success { get; set; }
        public string? Error { get; set; }
    }

    public class VKPhotoResponse
    {
        public string? PhotoId { get; set; }
        public bool Success { get; set; }
    }
}