using DataImportApp.Core.Interfaces;
using DataImportApp.Core.Services;
using DataImportApp.Infrastructure;

namespace DataImportApp.Importers
{
    public class CsvDataImporter : DataImporter
    {
        public CsvDataImporter(
            FileReader reader,
            ImportLogger logger,
            IDataValidator validator)
            : base(reader, logger, validator) { }

        protected override string Parse(string rawData)
        {
            Console.WriteLine("[CSV] Парсинг CSV");
            return rawData;
        }
    }
}