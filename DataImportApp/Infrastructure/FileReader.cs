namespace DataImportApp.Infrastructure
{
    public class FileReader
    {
        public string Read(string path)
        {
            Console.WriteLine($"[READ] {path}");
            return File.ReadAllText(path);
        }
    }
}