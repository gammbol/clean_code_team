using System;
using facade_good.Facades;

namespace facade_good;

public class Program
{
    public static void Main(string[] args)
    {
        Console.WriteLine("=== User Registration ===");

        var facade = new RegistrationFacade();

        Console.Write("Email: ");
        string email = Console.ReadLine() ?? "";

        Console.Write("Password: ");
        string password = Console.ReadLine() ?? "";

        bool success = facade.Register(email, password);

        if (success)
            Console.WriteLine("Registration completed");
    }
}