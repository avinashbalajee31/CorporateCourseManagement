using System.ComponentModel.DataAnnotations;

namespace CorporateCourseManagement.Models
{
    public class Feedback
    {
        [Key]
        public int Id { get; set; } 
        public string Name { get; set; } = null!;
        public string EmailId { get; set; } = null!;
        public string AboutWhom { get; set; } = null!;
        public string FeedbackInWords { get; set; } = null!;
        [Required]  
        public int Rating { get; set; } 

    }
}
