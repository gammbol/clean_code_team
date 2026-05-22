using System;

namespace MediatorPatternExample
{
    public class DefenseSecretary : Participant
    {
        public DefenseSecretary(string name, IThesisMediator mediator) : base(name, mediator) { }

        public void ProposeDefenseDate(string studentName, DateTime date)
        {
            Console.WriteLine($"[Секретарь: {Name}] Предлагаю дату защиты {date:dd.MM.yyyy HH:mm} для студента {studentName}.");
            Mediator.ScheduleDefense(Name, studentName, date);
        }

        public void AnnounceDefenseResult(string studentName, bool passed)
        {
            string result = passed ? "СДАЛ" : "НЕ СДАЛ";
            Console.WriteLine($"[Секретарь: {Name}] Официальный результат защиты студента {studentName}: {result}.");
            Mediator.NotifyDefenseOutcome(studentName, passed);
        }
    }
}