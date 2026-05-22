using System;
using System.Collections.Generic;

namespace MediatorPatternExample
{
    internal class Program
    {
        private static void Main()
        {
            var coordinator = new ThesisCoordinator();

            // Создаём участников
            var student = new Student("Анна Иванова", coordinator);
            var reviewer1 = new Reviewer("Проф. Михаил Сергеев", coordinator);
            var reviewer2 = new Reviewer("Проф. Елена Петрова", coordinator);
            var supervisor = new Supervisor("Д-р. Ольга Смирнова", coordinator);
            var secretary = new DefenseSecretary("Андрей Захаров", coordinator);

            // Регистрация
            coordinator.RegisterStudent(student);
            coordinator.RegisterReviewer(reviewer1);
            coordinator.RegisterReviewer(reviewer2);
            coordinator.RegisterSupervisor(supervisor);
            coordinator.RegisterDefenseSecretary(secretary);

            Console.WriteLine("\n--- Шаг 1: Подача дипломной работы ---");
            student.SubmitThesis("Применение ИИ в медицине");

            Console.WriteLine("\n--- Шаг 2: Рецензенты дают отзывы ---");
            reviewer1.SendReview("Анна Иванова", "Хорошая структура, но не хватает экспериментов.");
            reviewer2.SendReview("Анна Иванова", "Тема актуальна, требуется доработка выводов.");

            Console.WriteLine("\n--- Шаг 3: Руководитель просит доработку ---");
            supervisor.RequestRevision("Анна Иванова", "Добавьте сравнительный анализ с существующими методами.");

            Console.WriteLine("\n--- Шаг 4: Студент отправляет исправленную версию ---");
            student.SubmitRevisedThesis("Применение ИИ в медицине (исправленная)");

            Console.WriteLine("\n--- Шаг 5: Повторные отзывы рецензентов ---");
            reviewer1.SendReview("Анна Иванова", "Принято, работа значительно улучшена.");
            reviewer2.SendReview("Анна Иванова", "Замечания устранены, допускаю к защите.");

            Console.WriteLine("\n--- Шаг 6: Одобрение рецензентами и руководителем ---");
            reviewer1.FinalApprove("Анна Иванова", true);
            reviewer2.FinalApprove("Анна Иванова", true);
            supervisor.ApproveThesis("Анна Иванова");

            Console.WriteLine("\n--- Шаг 7: Секретарь назначает дату защиты (эмуляция) ---");
            // Защита уже планируется автоматически после утверждения, но для наглядности секретарь действует сам
            secretary.ProposeDefenseDate("Анна Иванова", DateTime.Now.AddDays(10));

            Console.WriteLine("\n--- Шаг 8: Проведение защиты ---");
            secretary.AnnounceDefenseResult("Анна Иванова", true);

            Console.WriteLine("\n=== Конец демонстрации ===");
        }
    }
}