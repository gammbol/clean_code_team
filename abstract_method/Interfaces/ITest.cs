using method_test.Models;

namespace method_test.Interfaces
{
    // Интерфейс продукта (Product).
    // Определяет контракт для всех конкретных тестов: имя и метод Execute.
    // Клиентский код зависит только от этой абстракции.
    public interface ITest
    {
        string Name { get; }
        TestResult Execute(TestContext context);
    }
}