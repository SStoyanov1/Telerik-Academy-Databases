namespace StudentsSystem.Model
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Lecture
    {
        [Key]
        public int LectureId { get; set; }

        [Required]
        [MinLength(3)]
        [MaxLength(30)]
        public string LectureName { get; set; }

        [Required]
        [MinLength(3)]
        [MaxLength(1000)]
        public string Content { get; set; }

        [ForeignKey("Course")]
        public int CourseId { get; set; }

        public Course Course { get; set; }
    }
}
