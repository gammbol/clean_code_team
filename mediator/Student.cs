using System;

namespace MediatorPatternExample
{
    public class Student : Participant
    {
        public Student(string name, IThesisMediator mediator) : base(name, mediator) { }

        public void SubmitThesis(string thesisTitle)
        {
            Console.WriteLine($"[Студент: {Name}] Отправляю дипломную работу '{thesisTitle}' на рецензию.");
            Mediator.SubmitThesis(Name, thesisTitle);
        }

        public void SubmitRevisedThesis(string thesisTitle)
        {
            Console.WriteLine($"[Студент: {Name}] Отправляю ИСПРАВЛЕННУЮ версию '{thesisTitle}' после доработки.");
            Mediator.SubmitRevisedThesis(Name, thesisTitle);
        }

        public void ReceiveReview(string reviewerName, string reviewText)
        {
            Console.WriteLine($"[Студент: {Name}] Получил рецензию от {reviewerName}: \"{reviewText}\"");
        }

        public void ReceiveRevisionRequest(string supervisorName, string revisionNotes)
        {
            Console.WriteLine($"[Студент: {Name}] Получил запрос на доработку от {supervisorName}: \"{revisionNotes}\"");
        }

        public void ReceiveDefenseNotification(DateTime date)
        {
            Console.WriteLine($"[Студент: {Name}] Уведомлён: защита назначена на {date:dd.MM.yyyy HH:mm}.");
        }

        public void ReceiveDefenseResult(bool passed)
        {
            Console.WriteLine($"[Студент: {Name}] Результат защиты: {(passed ? "СДАЛ" : "НЕ СДАЛ")}.");
        }
    }
}