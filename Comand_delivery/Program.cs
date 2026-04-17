using System;
using System.Text;

class Program
{
    static void Main(string[] args)
    {
        Console.OutputEncoding = Encoding.UTF8;
        Console.InputEncoding = Encoding.UTF8;

        var scenarioManager = new ScenarioManager();

        scenarioManager.Run();

        Console.WriteLine("\nДемонстрация завершена. Нажмите любую клавишу для выхода...");
        Console.ReadKey();
    }
}