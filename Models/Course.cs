using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ELearningApplication.API.Models
{
    public class Course
    {

        [Key]
        public int CourseId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string ContentURl { get; set; }
        [ForeignKey("Instructor")]
        public int InstructorId { get; set; }
        public User Instructor { get; set; }
    }
}
