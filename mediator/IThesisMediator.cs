namespace MediatorPatternExample
{
    // Расширенный интерфейс посредника
    public interface IThesisMediator
    {
        void RegisterStudent(Student student);
        void RegisterReviewer(Reviewer reviewer);
        void RegisterSupervisor(Supervisor supervisor);
        void RegisterDefenseSecretary(DefenseSecretary secretary);

        void SubmitThesis(string studentName, string thesisTitle);
        void SubmitRevisedThesis(string studentName, string thesisTitle);

        void SendReview(string reviewerName, string studentName, string reviewText);
        void RequestRevision(string supervisorName, string studentName, string revisionNotes);
        void ApproveThesis(string supervisorName, string studentName);
        void FinalApproveThesis(string reviewerName, string studentName, bool approved);
        
        void ScheduleDefense(string secretaryName, string studentName, DateTime defenseDate);
        void NotifyDefenseOutcome(string studentName, bool passed);
    }
}