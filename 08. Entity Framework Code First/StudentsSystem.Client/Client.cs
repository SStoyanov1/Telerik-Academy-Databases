namespace StudentsSystem.Client
{
    using System;
    using System.Linq;
    using System.Collections.Generic;

    using StudentsSystem.Data;
    using StudentsSystem.Model;

    public class Client
    {
        static void Main()
        {
            StudentsSystemDbContex dbContex = new StudentsSystemDbContex();

            using (dbContex)
            {
                List<Student> students = new List<Student>()
                {
                    new Student{ Name = "Petar Petrov", FacultyNumber = "FN2444" },
                    new Student{ Name = "Martin Gigov", FacultyNumber = "FN2433" },
                    new Student{ Name = "Svetla Dineva", FacultyNumber = "FN2432" }

                };

                Course dataStructures = new Course
                {
                     CourseName = "Data Structures",
                     Description = "Fundamentals of data structures",
                     Students = students
                };

                dbContex.Courses.Add(dataStructures);

                dbContex.SaveChanges();
            }
        }
    }
}
