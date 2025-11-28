using System;
using System.Linq;
using Xunit;
using CourseManagementSystem.Models;
using CourseManagementSystem.Services;

public class CourseManagerTests
{
    // Test for adding and removing a course
    [Fact]
    public void AddAndRemoveCourse_ShouldUpdateCourseList()
    {
        var manager = new CourseManager();
        var course = new OnlineCourse("Introduction to Testing", "https://test.com/intro");

        manager.AddCourse(course);
        Assert.Single(manager.GetAllCourses());
        
        manager.RemoveCourse(course.Id);
        Assert.Empty(manager.GetAllCourses());
    }

    // Test for successfully assigning a teacher to a course
    [Fact]
    public void AssignTeacherToCourse_ShouldSucceed()
    {
        var manager = new CourseManager();
        var teacher = new Teacher("Dr. Grace Hopper", "grace@uni.edu", "IT");
        var course = new OfflineCourse("Compilers", "Room B201", 40);

        manager.AddTeacher(teacher);
        manager.AddCourse(course);

        var result = manager.AssignTeacherToCourse(course.Id, teacher.Id);

        Assert.True(result);
        Assert.Equal(teacher.Id, manager.GetCourse(course.Id).AssignedTeacher.Id);
    }

    // Test for retrieving all courses taught by a specific teacher
    [Fact]
    public void GetCoursesByTeacher_ShouldReturnCorrectCourses()
    {
        var manager = new CourseManager();
        var teacher1 = new Teacher("Prof. Alan Turing", "alan@uni.edu", "Math");
        var teacher2 = new Teacher("Prof. Marvin Minsky", "marvin@uni.edu", "AI");
        manager.AddTeacher(teacher1);
        manager.AddTeacher(teacher2);

        var courseA = new OnlineCourse("Discrete Math", "url1");
        var courseB = new OfflineCourse("Robotics", "D101", 20);
        var courseC = new OnlineCourse("Advanced Calculus", "url2");

        manager.AddCourse(courseA);
        manager.AddCourse(courseB);
        manager.AddCourse(courseC);

        manager.AssignTeacherToCourse(courseA.Id, teacher1.Id); 
        manager.AssignTeacherToCourse(courseC.Id, teacher1.Id); 
        manager.AssignTeacherToCourse(courseB.Id, teacher2.Id); 

        var teacher1Courses = manager.GetCoursesByTeacher(teacher1.Id);

        Assert.Equal(2, teacher1Courses.Count);
    }

    // Test to ensure OfflineCourse capacity limit is respected (Business Logic)
    [Fact]
    public void EnrollStudentInOfflineCourse_ShouldRespectCapacity()
    {
        var manager = new CourseManager();
        var course = new OfflineCourse("Small Seminar", "Library", 1); // Max capacity 1
        var student1 = new Student("S1", "s1@mail.com", "ID1");
        var student2 = new Student("S2", "s2@mail.com", "ID2");

        manager.AddCourse(course);
        manager.AddStudent(student1);
        manager.AddStudent(student2);

        // Enroll first student (should succeed)
        var result1 = manager.EnrollStudentInCourse(course.Id, student1.Id);
        Assert.True(result1);
        Assert.Single(course.Students); 

        // Attempt to enroll second student (should fail)
        var result2 = manager.EnrollStudentInCourse(course.Id, student2.Id);
        Assert.False(result2);
        Assert.Single(course.Students); 
    }

    // Test to ensure students cannot be duplicated in a course
    [Fact]
    public void EnrollStudent_ShouldNotAddDuplicate()
    {
        var manager = new CourseManager();
        var course = new OnlineCourse("History 101", "url"); 
        var student = new Student("S1", "s1@mail.com", "ID1");

        manager.AddCourse(course);
        manager.AddStudent(student);

        manager.EnrollStudentInCourse(course.Id, student.Id);
        manager.EnrollStudentInCourse(course.Id, student.Id); 

        Assert.Single(course.Students); 
    }
}
