using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ELearningApplication.API.Models
{
    public class Submission
    {
        [Key]
        public int SubmissionId { get; set; }
        [ForeignKey("Assessment")]
        public int AssessmentId { get; set; }
        [ForeignKey("StudentId")]
        public int StudentId { get; set; }
        public double Score { get; set; }

        public Assessment Assessment { get; set; }
        public User Student { get; set; }
    }
}
