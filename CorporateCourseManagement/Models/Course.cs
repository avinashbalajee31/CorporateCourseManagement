using System.ComponentModel.DataAnnotations;

namespace CorporateCourseManagement.Models
{
    public class Course
    {
        [Key]
        public int Id { get; set; } 
        public string CourseName { get; set; } = null!;

        [Required]
        public int CourseDurationInWeeks { get; set; } 
        public string CourseTrainer { get; set; } = null!;

    }
}
