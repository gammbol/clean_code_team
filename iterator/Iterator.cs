using System;

namespace IteratorPatternExample
{
    public static class MissionPrinter
    {
        public static void Print(IMissionIterator iterator)
        {
            Console.WriteLine("Расписание миссии:\n");
            while (iterator.HasNext())
                Console.WriteLine(iterator.GetNext());
        }
    }

    internal class Program
    {
        private static void Main()
        {
            System.Console.OutputEncoding = System.Text.Encoding.UTF8;
            MissionSchedule schedule = CreateBiggerMission();

            Console.WriteLine("ПРЯМОЙ ИТЕРАТОР\n");
            MissionPrinter.Print(schedule.CreateIterator());

            Console.WriteLine("\nОГРАНИЧЕННЫЙ ПО ВРЕМЕНИ ИТЕРАТОР (МАКС 20 ДНЕЙ!!!)\n");
                MissionPrinter.Print(schedule.CreateTimeBoundedIterator(20));

            Console.WriteLine("\nФИЛЬТР: продолжительность >= 10 дней\n");
            MissionPrinter.Print(schedule.CreateFilteredIterator(s => s.DurationInDays >= 10));

            Console.WriteLine("\nФИЛЬТР: название стадии содержит 'Тест'\n");
            MissionPrinter.Print(schedule.CreateFilteredIterator(s => s.StageName.Contains("Тест")));
        }

        private static MissionSchedule CreateBiggerMission()
        {
            var schedule = new MissionSchedule();
            schedule.AddStage(new MissionStage("Сборка Ракеты", 14));
            schedule.AddStage(new MissionStage("Тест Двигателей", 6));
            schedule.AddStage(new MissionStage("Симуляция Орбиты", 10));
            schedule.AddStage(new MissionStage("Подготовка к Пуску", 5));
            schedule.AddStage(new MissionStage("Тест Систем Навигации", 7));
            schedule.AddStage(new MissionStage("Тренировка Состава", 12));
            schedule.AddStage(new MissionStage("Обратный Отсчет", 2));
            return schedule;
        }
    }
}