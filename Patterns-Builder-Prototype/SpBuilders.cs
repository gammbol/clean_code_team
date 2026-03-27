namespace WebPageDesignPatterns
{
    // Специализированные строители
    public class BlogPageBuilder : BasicWebPageBuilder
    {
        public override WebPage Build()
        {
            if (!_page.MetaTags.ContainsKey("viewport"))
                _page.AddMetaTag("viewport", "width=device-width, initial-scale=1");
            if (!_page.MetaTags.ContainsKey("robots"))
                _page.AddMetaTag("robots", "index, follow");
            if (_page.ContentBlocks.Count == 0)
                _page.AddContentBlock("Нет записей – добавьте первую статью!");
            return base.Build();
        }
    }

    public class LandingPageBuilder : BasicWebPageBuilder
    {
        public override WebPage Build()
        {
            _page.AddStyle("background-color", "#f8f9fa");
            _page.AddStyle("font-family", "Arial, sans-serif");
            if (!_page.ContentBlocks.Any(b => b.Contains("CTA")))
                _page.AddContentBlock("Призыв к действию (CTA): Купить сейчас!");
            return base.Build();
        }
    }
}