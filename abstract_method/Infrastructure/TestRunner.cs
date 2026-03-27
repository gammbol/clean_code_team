using System;
using System.Collections.Generic;
using method_test.Creators;
using method_test.Models;

namespace method_test.Infrastructure
{
    // Инкапсулирует логику запуска серии тестов и формирования отчёта.
    // помогает разгрузить мейн - клиент только конфигурирует пайплайн, а TestRunner выполняет.
    public class TestRunner
    {
        private readonly List<TestSuite> _pipeline;

        public TestRunner(List<TestSuite> pipeline)
        {
            _pipeline = pipeline;
        }

        public (int passed, int failed) RunAll()
        {
            int passed = 0;
            int failed = 0;

            // цикл работает с абстракцией TestSuite а не с конкретными классами
            foreach (var suite in _pipeline)
            {
                var result = suite.RunSuite();
                if (result.IsPassed) passed++;
                else failed++;
            }

            return (passed, failed);
        }

        public static void PrintSummary(int passed, int failed)
        {
            Console.WriteLine("тесты выполнены");
            Console.WriteLine($"Пройдено: {passed}, Провалено: {failed}");
        }
    }
}