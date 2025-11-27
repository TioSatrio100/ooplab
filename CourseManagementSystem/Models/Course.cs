using System;
using System.Collections.Generic;
using System.Linq;

namespace CourseManagementSystem.Models
{
    public abstract class Course
    {
        public Guid Id { get; private set; }
        public string Title { get; set; }
        public Teacher AssignedTeacher { get; set; }

        private readonly List<Student> _students = new List<Student>();
        public IReadOnlyList<Student> Students => _students.AsReadOnly();

        public Course(string title)
        {
            Id = Guid.NewGuid();
            Title = title;
        }

        public void AssignTeacher(Teacher teacher)
        {
            AssignedTeacher = teacher;
        }

        public void EnrollStudent(Student student)
        {
            if (!_students.Any(s => s.Id == student.Id))
            {
                _students.Add(student);
            }
        }

        public void DropStudent(Student student)
        {
            _students.RemoveAll(s => s.Id == student.Id);
        }

        public abstract string GetCourseTypeDetails();
    }
}