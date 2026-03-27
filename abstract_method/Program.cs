using System;
using System.Collections.Generic;
using method_test.Creators;
using method_test.Infrastructure;

namespace method_test
{
    // Клиент (Client)
    // Отвечает только за конфигурацию-создание списка фабрик и запуск через TestRunner.
    // Не знает о конкретных классах тестов, работает только с абстракцией TestSuite.
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Симулятор автоматизированного тестирования\n");

            // Список конфигураций
            var pipeline = new List<TestSuite>
            {
                new UnitTestSuite(),
                new IntegrationTestSuite(),
                new PerformanceTestSuite()
            };

            var runner = new TestRunner(pipeline);
            var (passed, failed) = runner.RunAll();
            
            TestRunner.PrintSummary(passed, failed);
        }
    }
}