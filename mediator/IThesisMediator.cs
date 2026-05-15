namespace MediatorPatternExample
{
    // Интерфейс посредника
    public interface IThesisMediator
    {
        void RegisterStudent(Student student);

        void RegisterReviewer(Reviewer reviewer);

        void SubmitThesis(
            string studentName,
            string thesisTitle);

        void SendReview(
            string reviewerName,
            string studentName,
            string reviewText);
    }
}