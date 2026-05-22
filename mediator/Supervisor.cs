using System;

namespace MediatorPatternExample
{
    public class Supervisor : Participant
    {
        public Supervisor(string name, IThesisMediator mediator) : base(name, mediator) { }

        public void ReviewThesis(string studentName, string thesisTitle)
        {
            Console.WriteLine($"[Руководитель: {Name}] Просматриваю работу '{thesisTitle}' студента {studentName}.");
        }

        public void RequestRevision(string studentName, string revisionNotes)
        {
            Console.WriteLine($"[Руководитель: {Name}] Запрашиваю доработку у студента {studentName}.");
            Mediator.RequestRevision(Name, studentName, revisionNotes);
        }

        public void ApproveThesis(string studentName)
        {
            Console.WriteLine($"[Руководитель: {Name}] УТВЕРЖДАЮ работу студента {studentName}.");
            Mediator.ApproveThesis(Name, studentName);
        }
    }
}