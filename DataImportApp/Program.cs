using DataImportApp.Core.Services;
using DataImportApp.Importers;
using DataImportApp.Infrastructure;

namespace DataImportApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var reader = new FileReader();
            var logger = new ImportLogger();
            var validator = new BasicDataValidator();

            DataImporter importer;

            importer = new CsvDataImporter(reader, logger, validator);
            importer.Import("Files/data.csv");

            Console.WriteLine("\n---\n");

            importer = new JsonDataImporter(reader, logger, validator);
            importer.Import("Files/data.json");

            Console.WriteLine("\n---\n");

            importer = new XmlDataImporter(reader, logger, validator);
            importer.Import("Files/data.xml");
        }
    }
}