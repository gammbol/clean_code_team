namespace WebPageDesignPatterns
{
    // Интерфейс строителя
    public interface IWebPageBuilder
    {
        IWebPageBuilder SetTitle(string title);
        IWebPageBuilder SetAuthor(string author);
        IWebPageBuilder AddContentBlock(string block);
        IWebPageBuilder AddStyle(string key, string value);
        IWebPageBuilder AddMetaTag(string name, string content);
        WebPage Build();
    }

    // Строитель
    public class BasicWebPageBuilder : IWebPageBuilder
    {
        protected WebPage _page = new();

        public IWebPageBuilder SetTitle(string title)
        {
            _page.Title = title;
            return this;
        }

        public IWebPageBuilder SetAuthor(string author)
        {
            _page.Author = author;
            return this;
        }

        public IWebPageBuilder AddContentBlock(string block)
        {
            _page.AddContentBlock(block);
            return this;
        }

        public IWebPageBuilder AddStyle(string key, string value)
        {
            _page.AddStyle(key, value);
            return this;
        }

        public IWebPageBuilder AddMetaTag(string name, string content)
        {
            _page.AddMetaTag(name, content);
            return this;
        }

        public virtual WebPage Build() => _page;

        public void Reset() => _page = new WebPage();
    }
}