using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CorporateCourseManagement.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string EmailId { get; set; } = null!;
        public string Password { get; set; } = null!;

        //[SqlDefaultValue(DefaultValue = "getutcdate()")]
        //public string? Role { get; set; } 
        //public sbyte? Role { get; set; }
        public string? Role { get; set; } = "Trainee";
    }
}
