using System;
using System.Collections.Generic;

namespace IteratorPatternExample
{
    // Сервис вывода
    public static class MissionPrinter
    {
        public static void Print(
            IMissionIterator iterator)
        {
            Console.WriteLine(
                "Mission Schedule:\n");

            while (iterator.HasNext())
            {
                Console.WriteLine(
                    iterator.GetNext());
            }
        }
    }

    internal class Program
    {
        private static void Main(string[] args)
        {
            MissionSchedule schedule =
                CreateSchedule();

            Console.WriteLine("FORWARD ITERATOR\n");

            MissionPrinter.Print(
                schedule.CreateIterator());

            Console.WriteLine("\nREVERSE ITERATOR\n");

            MissionPrinter.Print(
                schedule.CreateReverseIterator());

            Console.WriteLine("\nEVEN INDEX ITERATOR\n");

            MissionPrinter.Print(
                schedule.CreateEvenIterator());

            Console.WriteLine("\nLONG DURATION ITERATOR\n");

            MissionPrinter.Print(
                schedule.CreateLongDurationIterator(10));
        }

        private static MissionSchedule CreateSchedule()
        {
            MissionSchedule schedule =
                new MissionSchedule();

            schedule.AddStage(
                new MissionStage(
                    "Rocket Assembly",
                    14));

            schedule.AddStage(
                new MissionStage(
                    "Engine Testing",
                    7));

            schedule.AddStage(
                new MissionStage(
                    "Orbital Simulation",
                    10));

            schedule.AddStage(
                new MissionStage(
                    "Launch Preparation",
                    5));

            return schedule;
        }

        private static IMissionIterator CreateIterator(
            MissionSchedule schedule)
        {
            return schedule.CreateIterator();
        }

        private static void PrintSchedule(
            IMissionIterator iterator)
        {
            MissionPrinter.Print(iterator);
        }
    }
}