using DataImportApp.Core.Interfaces;

namespace DataImportApp.Infrastructure
{
    public class BasicDataValidator : IDataValidator
    {
        public bool Validate(string data)
        {
            Console.WriteLine("[VALIDATION] Проверка данных...");
            return !string.IsNullOrWhiteSpace(data);
        }
    }
}