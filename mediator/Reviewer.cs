using System;

namespace MediatorPatternExample
{
    public class Reviewer : Participant
    {
        public Reviewer(string name, IThesisMediator mediator) : base(name, mediator) { }

        public void ReceiveThesis(string studentName, string thesisTitle)
        {
            Console.WriteLine($"[Рецензент: {Name}] Получил работу '{thesisTitle}' от студента {studentName} для проверки.");
        }

        public void ReceiveRevisedThesis(string studentName, string thesisTitle)
        {
            Console.WriteLine($"[Рецензент: {Name}] Получил ИСПРАВЛЕННУЮ версию '{thesisTitle}' от студента {studentName}.");
        }

        public void SendReview(string studentName, string reviewText)
        {
            Console.WriteLine($"[Рецензент: {Name}] Отправляю рецензию студенту {studentName}.");
            Mediator.SendReview(Name, studentName, reviewText);
        }

        public void FinalApprove(string studentName, bool approved)
        {
            string action = approved ? "ОДОБРЯЮ" : "ОТКЛОНЯЮ";
            Console.WriteLine($"[Рецензент: {Name}] {action} работу студента {studentName}.");
            Mediator.FinalApproveThesis(Name, studentName, approved);
        }
    }
}