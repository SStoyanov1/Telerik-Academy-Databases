namespace StudentsSystem.Data
{
    using System.Data.Entity;
    using StudentsSystem.Model;
    using StudentsSystem.Data.Migrations;

    public class StudentsSystemDbContex: DbContext
    {
        public StudentsSystemDbContex()
            : base("StudentsSystemConnection")
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<StudentsSystemDbContex, Configuration>());
        }

        public IDbSet<Course> Courses { get; set; }

        public IDbSet<Student> Students { get; set; }

        public IDbSet<Homework> Homeworks { get; set; }

        public IDbSet<Lecture> Lectures { get; set; }
    }
}
