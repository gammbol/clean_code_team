using System;
using System.Collections.Generic;

namespace MediatorPatternExample
{
    public class ThesisCoordinator : IThesisMediator
    {
        private readonly Dictionary<string, Student> _students = new();
        private readonly Dictionary<string, Reviewer> _reviewers = new();
        private readonly Dictionary<string, Supervisor> _supervisors = new();
        private DefenseSecretary _secretary;

        // Состояния дипломной работы (упрощённо)
        private readonly Dictionary<string, string> _thesisStatus = new(); // studentName -> status
        private readonly Dictionary<string, int> _approvalCount = new();   // сколько рецензентов одобрило

        // ---- Регистрация ----
        public void RegisterStudent(Student student)
        {
            if (!_students.ContainsKey(student.Name))
            {
                _students[student.Name] = student;
                Console.WriteLine($"[Координатор] Зарегистрирован студент '{student.Name}'.");
            }
        }

        public void RegisterReviewer(Reviewer reviewer)
        {
            if (!_reviewers.ContainsKey(reviewer.Name))
            {
                _reviewers[reviewer.Name] = reviewer;
                Console.WriteLine($"[Координатор] Зарегистрирован рецензент '{reviewer.Name}'.");
            }
        }

        public void RegisterSupervisor(Supervisor supervisor)
        {
            if (!_supervisors.ContainsKey(supervisor.Name))
            {
                _supervisors[supervisor.Name] = supervisor;
                Console.WriteLine($"[Координатор] Зарегистрирован руководитель '{supervisor.Name}'.");
            }
        }

        public void RegisterDefenseSecretary(DefenseSecretary secretary)
        {
            _secretary = secretary;
            Console.WriteLine($"[Координатор] Зарегистрирован секретарь защиты '{secretary.Name}'.");
        }

        // ---- Основные действия ----
        public void SubmitThesis(string studentName, string thesisTitle)
        {
            Console.WriteLine($"\n[Координатор] → Студент {studentName} подал работу '{thesisTitle}'.");
            _thesisStatus[studentName] = "На рецензии";
            _approvalCount[studentName] = 0;

            // Оповещаем всех рецензентов
            foreach (var reviewer in _reviewers.Values)
            {
                reviewer.ReceiveThesis(studentName, thesisTitle);
            }
            // Оповещаем руководителя
            foreach (var supervisor in _supervisors.Values)
            {
                supervisor.ReviewThesis(studentName, thesisTitle);
            }
        }

        public void SubmitRevisedThesis(string studentName, string thesisTitle)
        {
            Console.WriteLine($"\n[Координатор] → Студент {studentName} повторно подал работу '{thesisTitle}' (исправленную).");
            _thesisStatus[studentName] = "Повторная рецензия";
            _approvalCount[studentName] = 0;

            foreach (var reviewer in _reviewers.Values)
            {
                reviewer.ReceiveRevisedThesis(studentName, thesisTitle);
            }
            foreach (var supervisor in _supervisors.Values)
            {
                supervisor.ReviewThesis(studentName, thesisTitle);
            }
        }

        public void SendReview(string reviewerName, string studentName, string reviewText)
        {
            if (_students.ContainsKey(studentName))
            {
                Console.WriteLine($"[Координатор] Пересылаю рецензию от {reviewerName} студенту {studentName}.");
                _students[studentName].ReceiveReview(reviewerName, reviewText);
            }
        }

        public void RequestRevision(string supervisorName, string studentName, string revisionNotes)
        {
            if (_students.ContainsKey(studentName))
            {
                Console.WriteLine($"[Координатор] Руководитель {supervisorName} запрашивает доработку у студента {studentName}.");
                _thesisStatus[studentName] = "Требует доработки";
                _students[studentName].ReceiveRevisionRequest(supervisorName, revisionNotes);
            }
        }

        public void ApproveThesis(string supervisorName, string studentName)
        {
            if (_students.ContainsKey(studentName))
            {
                Console.WriteLine($"[Координатор] Руководитель {supervisorName} утвердил работу студента {studentName}.");
                // Если руководитель утвердил и все рецензенты одобрили – можно назначать защиту
                CheckAndScheduleDefense(studentName);
            }
        }

        public void FinalApproveThesis(string reviewerName, string studentName, bool approved)
        {
            if (!_students.ContainsKey(studentName)) return;

            if (approved)
            {
                _approvalCount[studentName]++;
                Console.WriteLine($"[Координатор] Рецензент {reviewerName} ОДОБРИЛ работу студента {studentName} (одобрений: {_approvalCount[studentName]}/{_reviewers.Count}).");
                CheckAndScheduleDefense(studentName);
            }
            else
            {
                Console.WriteLine($"[Координатор] Рецензент {reviewerName} ОТКЛОНИЛ работу студента {studentName}. Требуется повторная подача.");
                _thesisStatus[studentName] = "Отклонена рецензентом";
            }
        }

        // Вспомогательный метод – проверяем, можно ли назначать защиту
        private void CheckAndScheduleDefense(string studentName)
        {
            // Условие: есть хотя бы один руководитель (для простоты считаем, что один), и все рецензенты одобрили
            bool supervisorApproved = true; // Для упрощения: допустим, руководитель уже вызвал ApproveThesis
            bool allReviewersApproved = _reviewers.Count > 0 && _approvalCount[studentName] >= _reviewers.Count;

            if (supervisorApproved && allReviewersApproved && _thesisStatus[studentName] != "Защита назначена")
            {
                Console.WriteLine($"[Координатор] Все условия выполнены для студента {studentName}. Автоматически запрашиваю дату защиты у секретаря.");
                _thesisStatus[studentName] = "Защита назначена";
                // Секретарь сам вызовет ScheduleDefense через посредника
                _secretary?.ProposeDefenseDate(studentName, DateTime.Now.AddDays(14));
            }
        }

        // Назначение защиты (вызывается секретарём)
        public void ScheduleDefense(string secretaryName, string studentName, DateTime defenseDate)
        {
            if (_students.ContainsKey(studentName))
            {
                Console.WriteLine($"[Координатор] Секретарь {secretaryName} назначает защиту студенту {studentName} на {defenseDate:dd.MM.yyyy HH:mm}.");
                _students[studentName].ReceiveDefenseNotification(defenseDate);
            }
        }

        // Оповещение о результате защиты
        public void NotifyDefenseOutcome(string studentName, bool passed)
        {
            if (_students.ContainsKey(studentName))
            {
                Console.WriteLine($"[Координатор] Распространяю результат защиты студента {studentName}.");
                _students[studentName].ReceiveDefenseResult(passed);
                _thesisStatus[studentName] = passed ? "Защищена" : "Провалена";
            }
        }
    }
}