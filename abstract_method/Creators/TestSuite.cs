using System;
using method_test.Interfaces;
using method_test.Models;

namespace method_test.Creators
{
    // Создатель (Creator)
    // Абстрактный класс, объявляет фабричный метод CreateTest() и шаблонный метод RunSuite().
    // Инкапсулирует общую логику запуска: подготовка контекста, создание теста, выполнение, логирование.
    public abstract class TestSuite
    {
        // Factory Method 
        public abstract ITest CreateTest();

        // метод для создания контекста 
        protected virtual TestContext CreateContext()
        {
            return new TestContext();
        }

        // Основная бизнес-логика запуска
        public TestResult RunSuite()
        {
            Console.WriteLine($"{GetType().Name} - Запуск серии тестов...");

            // 1 подготовка контекста (разная для разных тестов)
            var context = CreateContext();

            // 2 создание через фабричный метод
            ITest test = CreateTest();

            // 3 выполнение
            var result = test.Execute(context);

            // 4 логирование результата
            LogResult(test.Name, result);

            return result;
        }

        private void LogResult(string testName, TestResult result)
        {
            string status = result.IsPassed ? "OK" : "FAIL";
            Console.WriteLine($"{testName} - Status: {status}, Time: {result.DurationMs}ms");
            if (!result.IsPassed && result.Error != null)
            {
                Console.WriteLine($"ERROR - {result.Error.Message}");
            }
            Console.WriteLine();
        }
    }
}