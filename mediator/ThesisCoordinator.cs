namespace MediatorPatternExample
{
    // Конкретный посредник
    public class ThesisCoordinator : IThesisMediator
    {
        private readonly Dictionary<string, Student> _students;

        private readonly Dictionary<string, Reviewer> _reviewers;

        public ThesisCoordinator()
        {
            _students = new Dictionary<string, Student>();

            _reviewers = new Dictionary<string, Reviewer>();
        }

        public void RegisterStudent(Student student)
        {
            if (!_students.ContainsKey(student.Name))
            {
                _students.Add(student.Name, student);
            }
        }

        public void RegisterReviewer(Reviewer reviewer)
        {
            if (!_reviewers.ContainsKey(reviewer.Name))
            {
                _reviewers.Add(reviewer.Name, reviewer);
            }
        }

        public void SubmitThesis(
            string studentName,
            string thesisTitle)
        {
            Console.WriteLine(
                $"\n[Координатор] {studentName} отправил дипломную работу: '{thesisTitle}'");

            foreach (Reviewer reviewer in _reviewers.Values)
            {
                reviewer.ReceiveThesis(
                    studentName,
                    thesisTitle);
            }
        }

        public void SendReview(
            string reviewerName,
            string studentName,
            string reviewText)
        {
            if (_students.ContainsKey(studentName))
            {
                _students[studentName]
                    .ReceiveReview(
                        reviewerName,
                        reviewText);
            }
        }
    }
}