namespace ReportGeneratorBridge
{
    public interface IReportRenderer
    {
        void BeginReport(string title);
        void AddSection(string heading, string content);
        void AddTable(IEnumerable<(string Column, string Value)> data);
        void EndReport();
        string GetFileExtension();
        void SaveToFile(string filePath);
    }
}