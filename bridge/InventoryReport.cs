namespace ReportGeneratorBridge
{
    public class InventoryReport : Report
    {
        private readonly Dictionary<string, int> _stockLevels;

        public InventoryReport(IReportRenderer renderer, string title, Dictionary<string, int> stockLevels)
            : base(renderer, title)
        {
            _stockLevels = stockLevels;
        }

        public override void Generate()
        {
            _renderer.BeginReport($"{_title} от {_date:d}");
            _renderer.AddSection("Текущие остатки", "Ниже приведены остатки товаров на складе");

            var tableData = _stockLevels.Select(s => (Column: s.Key, Value: s.Value.ToString())).ToList();
            _renderer.AddTable(tableData);

            var lowStock = _stockLevels.Where(s => s.Value < 10).Select(s => s.Key);
            if (lowStock.Any())
                _renderer.AddSection("Критические запасы", $"Требуется пополнение: {string.Join(", ", lowStock)}");
            else
                _renderer.AddSection("Статус", "Все позиции в достаточном количестве");

            _renderer.EndReport();
        }
    }
}