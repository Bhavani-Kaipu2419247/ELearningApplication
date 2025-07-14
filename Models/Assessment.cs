using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ELearningApplication.API.Models
{
    public class Assessment
    {
        [Key]
        public int AssessmentId { get; set; }
        [ForeignKey("Course")]
        public int CourseId { get; set; }
        public int MaxScore { get; set; }

        public Course Course { get; set; }
    }
}
