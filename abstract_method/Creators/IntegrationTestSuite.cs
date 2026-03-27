using method_test.Interfaces;
using method_test.Models;
using method_test.Products;

namespace method_test.Creators
{
    // Конкретный создатель (Concrete Creator) - фабрика для интеграционных тестов.
    // Переопределяет CreateTest() для возврата DatabaseIntegrationTest.
    // Настраивает контекст с параметрами подключения к тестовой БД.
    public class IntegrationTestSuite : TestSuite
    {
        public override ITest CreateTest()
        {
            return new DatabaseIntegrationTest();
        }

        protected override TestContext CreateContext()
        {
            return new TestContext
            {
                Environment = "Staging",
                ConnectionString = "Server=localhost;Db=Test;",
                TimeoutMs = 5000
            };
        }
    }
}