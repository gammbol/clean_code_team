namespace DataImportApp.Infrastructure
{
    public class ImportLogger
    {
        public void Log(string path)
        {
            Console.WriteLine($"[LOG] Импорт завершен: {path}");
        }
    }
}