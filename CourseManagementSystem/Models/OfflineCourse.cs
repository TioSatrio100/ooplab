namespace CourseManagementSystem.Models
{
    public class OfflineCourse : Course
    {
        public string Location { get; set; }
        public int MaxCapacity { get; set; }

        public OfflineCourse(string title, string location, int capacity)
            : base(title)
        {
            Location = location;
            MaxCapacity = capacity;
        }

        public override string GetCourseTypeDetails()
        {
            return $"Type: Offline, Location: {Location}, Capacity: {MaxCapacity}";
        }
    }
}