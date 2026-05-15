using DataImportApp.Core.Interfaces;
using DataImportApp.Core.Services;
using DataImportApp.Infrastructure;

namespace DataImportApp.Importers
{
    public class JsonDataImporter : DataImporter
    {
        public JsonDataImporter(
            FileReader reader,
            ImportLogger logger,
            IDataValidator validator)
            : base(reader, logger, validator) { }

        protected override string Parse(string rawData)
        {
            Console.WriteLine("[JSON] Парсинг JSON");
            return rawData;
        }
    }
}