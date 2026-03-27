namespace WebPageDesignPatterns
{
    // Модель веб страницы (прототип)
    public class WebPage : ICloneable
    {
        public string Title { get; set; } = string.Empty;
        public string Author { get; set; } = string.Empty;
        public List<string> ContentBlocks { get; private set; } = new();
        public Dictionary<string, string> Styles { get; private set; } = new();
        public Dictionary<string, string> MetaTags { get; private set; } = new();

        public WebPage() { }

        public WebPage(WebPage other)
        {
            Title = other.Title;
            Author = other.Author;
            ContentBlocks = new List<string>(other.ContentBlocks);
            Styles = new Dictionary<string, string>(other.Styles);
            MetaTags = new Dictionary<string, string>(other.MetaTags);
        }

        public object Clone() => new WebPage(this);

        public void AddContentBlock(string block) => ContentBlocks.Add(block);
        public void AddStyle(string key, string value) => Styles[key] = value;
        public void AddMetaTag(string name, string content) => MetaTags[name] = content;

        public void Display()
        {
            Console.WriteLine($"Веб-страница: {Title}");
            Console.WriteLine($"Автор: {Author}");
            Console.WriteLine("Содержимое (блоки):");
            foreach (var block in ContentBlocks)
                Console.WriteLine($"  • {block}");
            Console.WriteLine("Стили (CSS):");
            foreach (var kvp in Styles)
                Console.WriteLine($"  {kvp.Key}: {kvp.Value}");
            Console.WriteLine("Метатеги:");
            foreach (var kvp in MetaTags)
                Console.WriteLine($"  {kvp.Key} = {kvp.Value}");
            Console.WriteLine(new string('-', 40));
        }
    }
}