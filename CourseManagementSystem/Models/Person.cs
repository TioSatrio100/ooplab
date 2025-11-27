using System;

namespace CourseManagementSystem.Models
{
    public abstract class Person
    {
        public Guid Id { get; private set; }
        public string Name { get; set; }
        public string Email { get; set; }

        public Person(string name, string email)
        {
            Id = Guid.NewGuid();
            Name = name;
            Email = email;
        }

        public abstract string GetDetails();
    }
}