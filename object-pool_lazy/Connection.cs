namespace object_pool_lazy;

public class Connection
{
    public int Id { get; }
    public bool IsBusy { get; set; }

    public Connection(int id)
    {
        Id = id;
    }

    public void Open()
    {
        Console.WriteLine($"Connection {Id} opened");
    }

    public void Close()
    {
        Console.WriteLine($"Connection {Id} closed");
    }
    
    public void ExecuteQuery(string query)
    {
        Console.WriteLine($"Connection {Id} executing: {query}");
        Thread.Sleep(200);
    }
}