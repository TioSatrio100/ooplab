namespace CourseManagementSystem.Models
{
    public class Teacher : Person
    {
        public string Department { get; set; }

        public Teacher(string name, string email, string department)
            : base(name, email)
        {
            Department = department;
        }

        public override string GetDetails()
        {
            return $"Teacher: {Name}, Email: {Email}, Dept: {Department}";
        }
    }
}