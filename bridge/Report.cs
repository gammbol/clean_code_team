namespace ReportGeneratorBridge
{
    public abstract class Report
    {
        protected IReportRenderer _renderer;
        protected string _title;
        protected DateTime _date;

        protected Report(IReportRenderer renderer, string title)
        {
            _renderer = renderer;
            _title = title;
            _date = DateTime.Now;
        }

        public abstract void Generate();

        public void Save(string baseFileName)
        {
            string filePath = $"{baseFileName}{_renderer.GetFileExtension()}";
            _renderer.SaveToFile(filePath);
        }
    }
}