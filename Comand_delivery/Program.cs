using System;
using System.Text;

class Program
{
    static void Main(string[] args)
    {
        Console.OutputEncoding = Encoding.UTF8;
        Console.InputEncoding = Encoding.UTF8;

        Console.WriteLine("---------------");
        Console.WriteLine("СИСТЕМА УПРАВЛЕНИЯ ДОСТАВКОЙ ЕДЫ");
        Console.WriteLine("---------------");

        // Создаём менеджер сценариев
        var scenarioManager = new ScenarioManager();

        // Запускаем систему 
        scenarioManager.Run();

        // Интерактивное меню 
        ShowInteractiveMenu(scenarioManager);
    }



    // Интерактивное меню 
    static void ShowInteractiveMenu(ScenarioManager manager)
    {
        bool running = true;

        while (running)
        {
            Console.WriteLine("---------------");
            Console.WriteLine("МЕНЮ:");
            Console.WriteLine("  1. Создать новый заказ");
            Console.WriteLine("  2. Создать заказ с полной обработкой (макрокоманда)");
            Console.WriteLine("  3. Отменить последнюю операцию (Undo)");
            Console.WriteLine("  4. Показать статус системы");
            Console.WriteLine("  5. Показать историю заказов");
            Console.WriteLine("  6. Запустить демо-сценарий (несколько заказов подряд)");
            Console.WriteLine("  0. Выход (остановить систему)");
            Console.WriteLine("---------------");
            Console.Write("Выберите действие: ");

            string input = Console.ReadLine();

            try
            {
                switch (input)
                {
                    case "1":
                        manager.CreateSimpleOrder();
                        break;
                    case "2":
                        manager.CreateFullOrder();
                        break;
                    case "3":
                        manager.UndoLastOperation();
                        break;
                    case "4":
                        manager.PrintStatus();
                        break;
                    case "5":
                        manager.PrintOrderHistory();
                        break;
                    case "6":
                        manager.RunDemoScenario();
                        break;
                    case "0":
                        running = false;
                        Console.WriteLine("\nОстановка системы...");
                        manager.Stop();
                        Console.WriteLine("Система остановлена. До свидания!");
                        break;
                    default:
                        Console.WriteLine("Неверная команда. Попробуйте снова.");
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\nОшибка: {ex.Message}");
            }
        }
    }
}
