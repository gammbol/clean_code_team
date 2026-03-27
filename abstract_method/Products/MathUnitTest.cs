using System;
using System.Diagnostics;
using method_test.Interfaces;
using method_test.Models;

namespace method_test.Products
{
    // Конкретный продукт (Concrete Product): юнит-тест математических операций.
    // Реализует интерфейс ITest.
    // Инкапсулирует логику проверки вычислений и замер времени.
    public class MathUnitTest : ITest
    {
        public string Name => "Unit: Math Calculation";

        public TestResult Execute(TestContext context)
        {
            var stopwatch = Stopwatch.StartNew();
            try
            {
                int result = 2 + 2;
                if (result != 4) throw new Exception("Math logic failed");

                stopwatch.Stop();
                return new TestResult { IsPassed = true, DurationMs = stopwatch.ElapsedMilliseconds, Message = "Calculation correct" };
            }
            catch (Exception ex)
            {
                stopwatch.Stop();
                return new TestResult { IsPassed = false, DurationMs = stopwatch.ElapsedMilliseconds, Error = ex };
            }
        }
    }
}