namespace ReportGeneratorBridge
{
    public class BridgeDemo
    {
        public void Run()
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.WriteLine("Паттерн Bridge: генерация отчётов в HTML и Markdown\n");

            // Данные для отчётов
            var salesData = new Dictionary<string, decimal>
            {
                ["Ноутбук"] = 250_000m,
                ["Мышь"] = 12_500m,
                ["Клавиатура"] = 35_000m
            };

            var inventoryData = new Dictionary<string, int>
            {
                ["Ноутбук"] = 5,
                ["Мышь"] = 120,
                ["Клавиатура"] = 8
            };

            // Создаём реализации (форматы)
            IReportRenderer html = new HtmlRenderer();
            IReportRenderer markdown = new MarkdownRenderer();

            // Отчёт о продажах в двух форматах
            Report salesReportHtml = new SalesReport(html, "Отчёт о продажах", salesData);
            Report salesReportMd = new SalesReport(markdown, "Отчёт о продажах", salesData);

            Console.WriteLine("Генерация отчётов о продажах...");
            salesReportHtml.Generate();
            salesReportHtml.Save("SalesReport");
            salesReportMd.Generate();
            salesReportMd.Save("SalesReport");

            // Отчёт об остатках
            Report inventoryReportHtml = new InventoryReport(html, "Складской отчёт", inventoryData);
            Report inventoryReportMd = new InventoryReport(markdown, "Складской отчёт", inventoryData);

            Console.WriteLine("\nГенерация складских отчётов...");
            inventoryReportHtml.Generate();
            inventoryReportHtml.Save("InventoryReport");
            inventoryReportMd.Generate();
            inventoryReportMd.Save("InventoryReport");

            Console.WriteLine("\n Все файлы созданы в папке приложения");
        }
    }
}