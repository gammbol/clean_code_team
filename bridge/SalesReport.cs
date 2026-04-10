namespace ReportGeneratorBridge
{
    public class SalesReport : Report
    {
        private readonly Dictionary<string, decimal> _salesByProduct;

        public SalesReport(IReportRenderer renderer, string title, Dictionary<string, decimal> salesByProduct)
            : base(renderer, title)
        {
            _salesByProduct = salesByProduct;
        }

        public override void Generate()
        {
            _renderer.BeginReport($"{_title} от {_date:d}");
            _renderer.AddSection("Общая информация", $"Отчёт сгенерирован {_date:F}");

            var tableData = _salesByProduct.Select(p => (Column: p.Key, Value: $"{p.Value:C}")).ToList();
            tableData.Add(("Общий объём", $"{_salesByProduct.Values.Sum():C}"));
            _renderer.AddTable(tableData);

            _renderer.AddSection("Анализ", "Наибольшие продажи у продукта: " +
                _salesByProduct.OrderByDescending(p => p.Value).First().Key);
            _renderer.EndReport();
        }
    }
}