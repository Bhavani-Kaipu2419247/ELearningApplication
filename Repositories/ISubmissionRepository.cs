using ELearningApplication.API.Models;

namespace ELearningApplication.API.Repositories
{
    public interface ISubmissionRepository
    {
        Task<IEnumerable<Submission>> GetAllAsync();
        Task<Submission?> GetByIdAsync(int id);
        Task<Submission> CreateAsync(Submission submission);
        Task<bool> UpdateAsync(Submission submission);
        Task<bool> DeleteAsync(int id);
    }
}
