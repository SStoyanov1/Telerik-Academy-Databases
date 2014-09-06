namespace StudentsSystem.Data.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using StudentsSystem.Model;

    public sealed class Configuration : DbMigrationsConfiguration<StudentsSystemDbContex>
    {
        public Configuration()
        {
            this.AutomaticMigrationsEnabled = true;
            this.AutomaticMigrationDataLossAllowed = false;
            this.ContextKey = "StudentsSystem.Data.StudentsSystemDbContex";
        }

        protected override void Seed(StudentsSystemDbContex context)
        {
            this.SeedCourses(context);
        }

        private void SeedCourses(StudentsSystemDbContex context)
        {
            if (context.Courses.Any())
            {
                return;
            }

            context.Courses.Add(new Course
            {
                CourseName = "OOP",
                Description = "Object oriented programming"
            });
        }
    }
}
