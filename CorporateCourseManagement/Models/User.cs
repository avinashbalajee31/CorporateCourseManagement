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

        public sbyte? Role { get; set; }
    }
}
