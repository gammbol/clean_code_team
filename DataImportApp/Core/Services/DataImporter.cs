using DataImportApp.Core.Interfaces;
using DataImportApp.Infrastructure;

namespace DataImportApp.Core.Services
{
    public abstract class DataImporter
    {
        private readonly FileReader _reader;
        private readonly ImportLogger _logger;
        private readonly IDataValidator _validator;

        protected DataImporter(
            FileReader reader,
            ImportLogger logger,
            IDataValidator validator)
        {
            _reader = reader;
            _logger = logger;
            _validator = validator;
        }

        // TEMPLATE METHOD
        public void Import(string path)
        {
            if (!ValidateFile(path))
                return;

            var rawData = _reader.Read(path);

            var parsedData = Parse(rawData);

            if (!_validator.Validate(parsedData))
            {
                Console.WriteLine("[ERROR] Данные не прошли валидацию");
                return;
            }

            Save(parsedData);

            _logger.Log(path);
        }

        protected virtual bool ValidateFile(string path)
        {
            if (string.IsNullOrWhiteSpace(path))
            {
                Console.WriteLine("[ERROR] Путь пуст");
                return false;
            }

            if (!File.Exists(path))
            {
                Console.WriteLine("[ERROR] Файл не найден");
                return false;
            }

            return true;
        }

        // ключевой шаг (Template Method)
        protected abstract string Parse(string rawData);

        protected virtual void Save(string data)
        {
            Console.WriteLine("[SAVE] Данные сохранены");
        }
    }
}