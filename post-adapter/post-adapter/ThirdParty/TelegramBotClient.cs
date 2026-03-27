namespace CrossPostingSystem.ThirdParty
{
    // Эмуляция стороннего Telegram Bot API
    public class TelegramBotClient
    {
        private readonly string _botToken;
        private readonly long _channelId;

        public TelegramBotClient(string botToken, long channelId)
        {
            _botToken = botToken;
            _channelId = channelId;
        }

        // Метод Telegram API - отправка сообщения
        public TelegramMessageResponse SendMessage(string text, string parseMode = "HTML")
        {
            Console.WriteLine($"[Telegram API] Отправка в канал {_channelId}");
            Console.WriteLine($"[Telegram API] Текст: {text}");

            System.Threading.Thread.Sleep(250);

            return new TelegramMessageResponse
            {
                MessageId = new Random().Next(1000, 9999),
                Success = true,
                Date = DateTime.Now
            };
        }

        // Метод Telegram API - отправка фото
        public TelegramPhotoResponse SendPhoto(string photoUrl, string caption = null)
        {
            Console.WriteLine($"[Telegram API] Отправка фото: {photoUrl}");
            if (!string.IsNullOrEmpty(caption))
                Console.WriteLine($"[Telegram API] Подпись: {caption}");

            System.Threading.Thread.Sleep(300);

            return new TelegramPhotoResponse
            {
                MessageId = new Random().Next(1000, 9999),
                Success = true
            };
        }

        public bool TestConnection()
        {
            Console.WriteLine("[Telegram API] Проверка соединения с API...");
            return _botToken.Contains(":");
        }
    }

    public class TelegramMessageResponse
    {
        public int MessageId { get; set; }
        public bool Success { get; set; }
        public DateTime Date { get; set; }
    }

    public class TelegramPhotoResponse
    {
        public int MessageId { get; set; }
        public bool Success { get; set; }
    }
}