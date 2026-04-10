namespace ReportGeneratorBridge
{
    public class MarkdownRenderer : IReportRenderer
    {
        private readonly List<string> _mdContent = new();

        public void BeginReport(string title)
        {
            _mdContent.Clear();
            _mdContent.Add($"# {title}");
            _mdContent.Add("");
        }

        public void AddSection(string heading, string content)
        {
            _mdContent.Add($"## {heading}");
            _mdContent.Add(content);
            _mdContent.Add("");
        }

        public void AddTable(IEnumerable<(string Column, string Value)> data)
        {
            _mdContent.Add("| Показатель | Значение |");
            _mdContent.Add("|------------|----------|");
            foreach (var row in data)
            {
                _mdContent.Add($"| {row.Column} | {row.Value} |");
            }
            _mdContent.Add("");
        }

        public void EndReport() { }

        public string GetFileExtension() => ".md";

        public void SaveToFile(string filePath)
        {
            File.WriteAllLines(filePath, _mdContent);
            Console.WriteLine($"Markdown-отчёт сохранён: {Path.GetFullPath(filePath)}");
        }
    }
}