namespace StudentsSystem.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Course
    {
        private ICollection<Student> students;

        private ICollection<Homework> homeworks;

        private ICollection<Lecture> lectures;

        public Course()
        {
            this.students = new HashSet<Student>();
            this.homeworks = new HashSet<Homework>();
            this.lectures = new HashSet<Lecture>();
        }

        [Key]
        public int CourseId { get; set; }

        [Required]
        [MinLength(3)]
        [MaxLength(20)]
        public string CourseName { get; set; }

        [MinLength(3)]
        [MaxLength(50)]
        public string Description { get; set; }

        public virtual ICollection<Student> Students
        {
            get { return this.students; }
            set { this.students = value; }
        }

        public virtual ICollection<Homework> Homeworks
        {
            get { return this.homeworks; }
            set { this.homeworks = value; }
        }

        public virtual ICollection<Lecture> Lectures
        {
            get { return this.lectures; }
            set { this.lectures = value; }
        }
    }
}
