using System;
using System.Diagnostics;
using method_test.Interfaces;
using method_test.Models;

namespace method_test.Products
{
    //  онкретный продукт (Concrete Product): нагрузочный тест производительности.
    // ¬ыполн€ет цикл операций и сравнивает врем€ выполнени€ с таймаутом из контекста.
    // ¬озвращает результат с флагом успешности и длительностью.
    public class LoadPerformanceTest : ITest
    {
        public string Name => "Performance: Load Under Stress";

        public TestResult Execute(TestContext context)
        {
            var stopwatch = Stopwatch.StartNew();
            try
            {
                long operations = 0;
                // логика нагрузочного теста - цикл операций
                for (int i = 0; i < 1000000; i++)
                {
                    operations += i;
                }

                stopwatch.Stop();
                bool isFastEnough = stopwatch.ElapsedMilliseconds < context.TimeoutMs;

                return new TestResult
                {
                    IsPassed = isFastEnough,
                    DurationMs = stopwatch.ElapsedMilliseconds,
                    Message = isFastEnough ? "Within timeout" : "Timeout exceeded"
                };
            }
            catch (Exception ex)
            {
                stopwatch.Stop();
                return new TestResult { IsPassed = false, DurationMs = stopwatch.ElapsedMilliseconds, Error = ex };
            }
        }
    }
}