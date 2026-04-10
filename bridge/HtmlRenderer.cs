namespace ReportGeneratorBridge
{
    public class HtmlRenderer : IReportRenderer
    {
        private readonly List<string> _htmlContent = new();

        public void BeginReport(string title)
        {
            _htmlContent.Clear();
            _htmlContent.Add("<!DOCTYPE html>");
            _htmlContent.Add("<html><head><meta charset='utf-8'>");
            _htmlContent.Add($"<title>{title}</title>");
            _htmlContent.Add("<style>body { font-family: Arial; } table { border-collapse: collapse; } td, th { border: 1px solid #ddd; padding: 8px; }</style>");
            _htmlContent.Add("</head><body>");
            _htmlContent.Add($"<h1>{title}</h1>");
        }

        public void AddSection(string heading, string content)
        {
            _htmlContent.Add($"<h2>{heading}</h2>");
            _htmlContent.Add($"<p>{content}</p>");
        }

        public void AddTable(IEnumerable<(string Column, string Value)> data)
        {
            _htmlContent.Add("<table>");
            _htmlContent.Add("<tr><th>Показатель</th><th>Значение</th></tr>");
            foreach (var row in data)
            {
                _htmlContent.Add($"<tr><td>{row.Column}</td><td>{row.Value}</td></tr>");
            }
            _htmlContent.Add("</table>");
        }

        public void EndReport()
        {
            _htmlContent.Add("</body></html>");
        }

        public string GetFileExtension() => ".html";

        public void SaveToFile(string filePath)
        {
            File.WriteAllLines(filePath, _htmlContent);
            Console.WriteLine($"HTML-отчёт сохранён: {Path.GetFullPath(filePath)}");
        }
    }
}