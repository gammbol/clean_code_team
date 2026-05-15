using DataImportApp.Core.Interfaces;
using DataImportApp.Core.Services;
using DataImportApp.Infrastructure;

namespace DataImportApp.Importers
{
    public class XmlDataImporter : DataImporter
    {
        public XmlDataImporter(
            FileReader reader,
            ImportLogger logger,
            IDataValidator validator)
            : base(reader, logger, validator) { }

        protected override string Parse(string rawData)
        {
            Console.WriteLine("[XML] Парсинг XML");
            return rawData;
        }
    }
}