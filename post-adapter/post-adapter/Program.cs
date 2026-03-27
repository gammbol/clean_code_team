using CrossPostingSystem.Models;
using CrossPostingSystem.Services;

namespace CrossPostingSystem
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            RunCrossPostingDemo();

            Console.WriteLine("Нажмите любую клавишу для выхода...");
            Console.ReadKey();
        }

        // Точка входа: запускает демонстрацию кросспостинга
        private static void RunCrossPostingDemo()
        {
            Console.WriteLine("СИСТЕМА КРОССПОСТИНГА В СОЦИАЛЬНЫЕ СЕТИ");
            Console.WriteLine("------------------------------------\n");

            var service = ConfigurePublishingService();
            var post = CreateSamplePost();

            service.Publish(post);
        }

        // Настраивает сервис кросспостинга: создаёт клиентов, адаптеры, подключает сети
        private static CrossPostingService ConfigurePublishingService()
        {
            var service = new CrossPostingService();

            // Инициализация сторонних клиентов (эмуляция SDK)
            var vkClient = new ThirdParty.VKApiClient("vk_token_abc123xyz", 123456);
            var tgClient = new ThirdParty.TelegramBotClient("telegram_bot:token123", -1001234567);
            var igClient = new ThirdParty.InstagramApiClient("instagram_long_access_token_here", "17841400001234567");

            // Создание адаптеров (АДАПТЕР: преобразуем несовместимые API к IPublisher)
            var vkAdapter = new Adapters.VKAdapter(vkClient, 123456);
            var tgAdapter = new Adapters.TelegramAdapter(tgClient, -1001234567);
            var igAdapter = new Adapters.InstagramAdapter(igClient, "17841400001234567");

            // Подключение сетей к сервису
            service.AddPublisher(vkAdapter);
            service.AddPublisher(tgAdapter);
            service.AddPublisher(igAdapter);

            service.ShowConnectedNetworks();

            return service;
        }

        // Создаёт тестовый пост для демонстрации
        private static Post CreateSamplePost()
        {
            var post = new Post(
                content: "Запуск нашего нового продукта!\nСледите за обновлениями.",
                imageUrl: "https://images/product.jpg"
            );

            post.AddTag("новинка");
            post.AddTag("запуск");
            post.AddTag("продукт");

            return post;
        }
    }
}