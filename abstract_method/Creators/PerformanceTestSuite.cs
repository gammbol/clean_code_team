using method_test.Interfaces;
using method_test.Models;
using method_test.Products;

namespace method_test.Creators
{
    // Конкретный создатель (Concrete Creator) - фабрика для тестов производительности.
    // Переопределяет CreateTest() для возврата LoadPerformanceTest.
    // Настраивает контекст с увеличенным таймаутом для имитации продакшн-окружения.
    public class PerformanceTestSuite : TestSuite
    {
        public override ITest CreateTest()
        {
            return new LoadPerformanceTest();
        }

        protected override TestContext CreateContext()
        {
            return new TestContext { Environment = "Production Mirror", TimeoutMs = 2000 };
        }
    }
}