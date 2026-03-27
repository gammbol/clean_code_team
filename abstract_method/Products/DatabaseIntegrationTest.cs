using System;
using System.Diagnostics;
using method_test.Interfaces;
using method_test.Models;

namespace method_test.Products
{
    // Конкретный продукт (Concrete Product): интеграционный тест подключения к БД.
    // Проверяет наличие строки подключения в контексте и имитирует сетевой вызов.
    // Зависит от TestContext для получения конфигурации.
    public class DatabaseIntegrationTest : ITest
    {
        public string Name => "Integration: DB Connection";

        public TestResult Execute(TestContext context)
        {
            var stopwatch = Stopwatch.StartNew();
            try
            {
                // Имитация работы - проверка конфигурации подключения
                if (string.IsNullOrEmpty(context.ConnectionString))
                {
                    throw new Exception("Connection string is missing in context");
                }

                System.Threading.Thread.Sleep(100);

                stopwatch.Stop();
                return new TestResult { IsPassed = true, DurationMs = stopwatch.ElapsedMilliseconds, Message = "DB Connected" };
            }
            catch (Exception ex)
            {
                stopwatch.Stop();
                return new TestResult { IsPassed = false, DurationMs = stopwatch.ElapsedMilliseconds, Error = ex };
            }
        }
    }
}