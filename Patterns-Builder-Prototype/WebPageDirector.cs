namespace WebPageDesignPatterns
{
    // Директор
    public class WebPageDirector
    {
        private readonly IWebPageBuilder _builder;

        public WebPageDirector(IWebPageBuilder builder) => _builder = builder;

        public WebPage BuildCorporateSite()
        {
            return _builder
                .SetTitle("Корпоративный сайт")
                .SetAuthor("Отдел маркетинга")
                .AddContentBlock("О компании")
                .AddContentBlock("Наши услуги")
                .AddContentBlock("Контакты")
                .AddStyle("color", "#333")
                .AddMetaTag("description", "Официальный сайт компании")
                .Build();
        }

        public WebPage BuildPromoPage()
        {
            return _builder
                .SetTitle("Акция!")
                .SetAuthor("Промо-отдел")
                .AddContentBlock("Скидка 50% на первый заказ")
                .AddContentBlock("Подробности здесь...")
                .AddStyle("font-size", "24px")
                .AddStyle("text-align", "center")
                .AddMetaTag("keywords", "скидка, акция, распродажа")
                .Build();
        }
    }
}