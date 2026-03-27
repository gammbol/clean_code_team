using method_test.Interfaces;
using method_test.Models;
using method_test.Products;

namespace method_test.Creators
{
    // Конкретный создатель (Concrete Creator) - фабрика для юнит-тестов.
    // Переопределяет CreateTest() для возврата MathUnitTest.
    // Настраивает лёгкий контекст (Sandbox, короткий таймаут).
    public class UnitTestSuite : TestSuite
    {
        public override ITest CreateTest()
        {
            return new MathUnitTest();
        }

        protected override TestContext CreateContext()
        {
            return new TestContext { Environment = "Sandbox", TimeoutMs = 100 };
        }
    }
}