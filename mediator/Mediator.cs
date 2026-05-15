using System;
using System.Collections.Generic;

namespace MediatorPatternExample
{
    // Студент
    public class Student : Participant
    {
        public Student(
            string name,
            IThesisMediator mediator)
            : base(name, mediator)
        {
        }

        public void SubmitThesis(string thesisTitle)
        {
            Mediator.SubmitThesis(
                Name,
                thesisTitle);
        }

        public void ReceiveReview(
            string reviewerName,
            string reviewText)
        {
            Console.WriteLine(
                $"[Студент: {Name}] Получил оценку от {reviewerName}: {reviewText}");
        }
    }

    // Рецензент
    public class Reviewer : Participant
    {
        public Reviewer(
            string name,
            IThesisMediator mediator)
            : base(name, mediator)
        {
        }

        public void ReceiveThesis(
            string studentName,
            string thesisTitle)
        {
            Console.WriteLine(
                $"[Рецезент: {Name}] Получил дипломную работу '{thesisTitle}' от {studentName}");
        }

        public void SendReview(
            string studentName,
            string reviewText)
        {
            Mediator.SendReview(
                Name,
                studentName,
                reviewText);
        }
    }

    internal class Program
    {
        private static void Main(string[] args)
        {
            ThesisCoordinator coordinator =
                CreateCoordinator();

            Student student =
                CreateStudent(coordinator);

            List<Reviewer> reviewers =
                CreateReviewers(coordinator);

            RegisterParticipants(
                coordinator,
                student,
                reviewers);

            RunScenario(
                student,
                reviewers);
        }

        private static ThesisCoordinator CreateCoordinator()
        {
            return new ThesisCoordinator();
        }

        private static Student CreateStudent(
            ThesisCoordinator coordinator)
        {
            return new Student(
                "Баха",
                coordinator);
        }

        private static List<Reviewer> CreateReviewers(
            ThesisCoordinator coordinator)
        {
            return new List<Reviewer>
            {
                new Reviewer(
                    "Профессор Михаил",
                    coordinator),

                new Reviewer(
                    "Профессор Мухаммед",
                    coordinator)
            };
        }

        private static void RegisterParticipants(
            ThesisCoordinator coordinator,
            Student student,
            List<Reviewer> reviewers)
        {
            coordinator.RegisterStudent(student);

            foreach (Reviewer reviewer in reviewers)
            {
                coordinator.RegisterReviewer(reviewer);
            }
        }

        private static void RunScenario(
            Student student,
            List<Reviewer> reviewers)
        {
            student.SubmitThesis(
                "Машинное обучение в здравоохранении");

            reviewers[0].SendReview(
                "Баха",
                "Отличная форма! Добавь больше статистики.");

            reviewers[1].SendReview(
                "Баха",
                "Исправь заключение.");
        }
    }
}