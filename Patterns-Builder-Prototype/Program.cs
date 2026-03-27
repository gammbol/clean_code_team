using System;
using System.Collections.Generic;
using System.Linq;

namespace WebPageDesignPatterns
{
    public class WebPageDemo
    {
        public void Run()
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.WriteLine("Паттерн Builder – создание веб-страниц\n");

            DemoBuilderDirectUsage();
            DemoDirectorWithSpecializedBuilders();
            DemoPrototypeCloning();
            DemoSeriesCreationFromTemplate();
        }

        private void DemoBuilderDirectUsage()
        {
            var builder = new BasicWebPageBuilder();
            var aboutPage = builder
                .SetTitle("О нас")
                .SetAuthor("Команда")
                .AddContentBlock("Мы занимаемся разработкой ПО с 2010 года.")
                .AddContentBlock("Наши клиенты – ведущие компании.")
                .AddStyle("background-color", "#eef")
                .AddMetaTag("description", "Страница о компании")
                .Build();

            Console.WriteLine("Страница 'О нас' (создана через строитель):");
            aboutPage.Display();
        }

        private void DemoDirectorWithSpecializedBuilders()
        {
            var blogDirector = new WebPageDirector(new BlogPageBuilder());
            var blogPage = blogDirector.BuildCorporateSite();
            Console.WriteLine("Блоговая страница (через директор):");
            blogPage.Display();

            var landingDirector = new WebPageDirector(new LandingPageBuilder());
            var landingPage = landingDirector.BuildPromoPage();
            Console.WriteLine("Лендинг (через директор):");
            landingPage.Display();
        }

        private void DemoPrototypeCloning()
        {
            Console.WriteLine("\nПаттерн Prototype – клонирование страниц\n");

            // Создаём прототип
            var builder = new BasicWebPageBuilder();
            var prototype = builder
                .SetTitle("О нас")
                .SetAuthor("Команда")
                .AddContentBlock("Мы занимаемся разработкой ПО с 2010 года.")
                .AddContentBlock("Наши клиенты – ведущие компании.")
                .AddStyle("background-color", "#eef")
                .AddMetaTag("description", "Страница о компании")
                .Build();

            Console.WriteLine("Исходный прототип:");
            prototype.Display();

            // Клонируем и модифицируем
            WebPage clonedPage = (WebPage)prototype.Clone();
            clonedPage.Title = "О нас (копия)";
            clonedPage.AddContentBlock("Добавлен новый блок в клоне");
            clonedPage.AddStyle("border", "1px solid #ccc");

            Console.WriteLine("Клон с изменениями:");
            clonedPage.Display();

            Console.WriteLine("Оригинал остался без изменений:");
            prototype.Display();
        }

        private void DemoSeriesCreationFromTemplate()
        {
            Console.WriteLine("\nСоздание серии страниц на основе шаблона:");
            var template = new BasicWebPageBuilder()
                .SetTitle("Шаблон")
                .SetAuthor("Система")
                .AddContentBlock("Стандартный блок")
                .Build();

            var pages = new List<WebPage>();
            for (int i = 1; i <= 3; i++)
            {
                var copy = (WebPage)template.Clone();
                copy.Title = $"Страница {i}";
                copy.AddMetaTag("page-id", i.ToString());
                pages.Add(copy);
            }

            foreach (var page in pages)
                page.Display();
        }
    }

    // Main (НЕ ПРОСТЫНЯ)
    class Program
    {
        static void Main(string[] args)
        {
            var demo = new WebPageDemo();
            demo.Run();
        }
    }
}