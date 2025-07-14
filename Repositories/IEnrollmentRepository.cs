using ELearningApplication.API.Models;

namespace ELearningApplication.API.Repositories
{
    public interface IEnrollmentRepository
    {
        Task<Enrollment?> GetEnrollmentByIdAsync(int id);
        Task<IEnumerable<Enrollment>> GetEnrollmentsByStudentAsync(int studentId);
        Task<Enrollment> AddEnrollmentAsync(Enrollment enrollment);
        Task<bool> EnrollmentExistsAsync(int studentId, int courseId);
        Task<bool> UpdateEnrollmentAsync(Enrollment enrollment);
        Task<bool> DeleteEnrollmentAsync(int id);
    }
}
