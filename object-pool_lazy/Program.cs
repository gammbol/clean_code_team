namespace object_pool_lazy;

class Program
{
    private static LazyInitializer<ConnectionPool> _pool =
        new LazyInitializer<ConnectionPool>(() => new ConnectionPool(3));

    
    static void Main(string[] args)
    {
        var handler = new RequestHandler(_pool.Value);

        for (int i = 1; i <= 10; i++)
        {
            Console.WriteLine($"\nRequest {i}");
            
            handler.HandleRequest("SELECT * FROM users WHERE id = {i}");
        }

        Console.WriteLine("\nAll requests have been processed!");
    }
}
