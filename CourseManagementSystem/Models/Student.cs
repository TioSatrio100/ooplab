namespace CourseManagementSystem.Models
{
    public class Student : Person
    {
        public string StudentId { get; private set; }

        public Student(string name, string email, string studentId)
            : base(name, email)
        {
            StudentId = studentId;
        }

        public override string GetDetails()
        {
            return $"Student: {Name}, ID: {StudentId}, Email: {Email}";
        }
    }
}