namespace facade_bad.Infrastructure;

public class Logger
{
    public void Info(string message)
    {
        Console.WriteLine($"[INFO] {message}");
    }
}