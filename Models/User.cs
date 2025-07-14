using System.ComponentModel.DataAnnotations;

namespace ELearningApplication.API.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; }
        public string Name { get; set; }
        [Required]
        [RegularExpression("student|instructor", ErrorMessage = "Role must be either 'student' or 'instructor'")]
        public string Role { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
