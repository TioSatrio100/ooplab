using System;
using System.Collections.Generic;
using System.Linq;
using CourseManagementSystem.Models;

namespace CourseManagementSystem.Services
{
    public class CourseManager
    {
        private readonly List<Course> _courses = new List<Course>();
        private readonly List<Teacher> _teachers = new List<Teacher>();
        private readonly List<Student> _students = new List<Student>();

        public CourseManager() { }

        public void AddCourse(Course course) => _courses.Add(course);
        public void RemoveCourse(Guid id) => _courses.RemoveAll(c => c.Id == id);
        public Course GetCourse(Guid id) => _courses.FirstOrDefault(c => c.Id == id);
        public IReadOnlyList<Course> GetAllCourses() => _courses.AsReadOnly();

        public void AddTeacher(Teacher teacher) => _teachers.Add(teacher);
        public Teacher GetTeacher(Guid id) => _teachers.FirstOrDefault(t => t.Id == id);
        public IReadOnlyList<Teacher> GetAllTeachers() => _teachers.AsReadOnly();

        public void AddStudent(Student student) => _students.Add(student);
        public Student GetStudent(Guid id) => _students.FirstOrDefault(s => s.Id == id);
        public IReadOnlyList<Student> GetAllStudents() => _students.AsReadOnly();

        public bool AssignTeacherToCourse(Guid courseId, Guid teacherId)
        {
            var course = GetCourse(courseId);
            var teacher = GetTeacher(teacherId);

            if (course != null && teacher != null)
            {
                course.AssignTeacher(teacher);
                return true;
            }
            return false;
        }

        public IReadOnlyList<Course> GetCoursesByTeacher(Guid teacherId)
        {
            return _courses
                .Where(c => c.AssignedTeacher?.Id == teacherId)
                .ToList()
                .AsReadOnly();
        }

        public bool EnrollStudentInCourse(Guid courseId, Guid studentId)
        {
            var course = GetCourse(courseId);
            var student = GetStudent(studentId);

            if (course != null && student != null)
            {
                if (course is OfflineCourse offlineCourse && offlineCourse.Students.Count >= offlineCourse.MaxCapacity)
                {
                    return false;
                }

                course.EnrollStudent(student);
                return true;
            }
            return false;
        }
    }
}