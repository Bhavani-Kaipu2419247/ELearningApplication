using ELearningApplication.API.Models;

namespace ELearningApplication.API.Repositories
{
    public interface IAssessmentRepository
    {
        Task<IEnumerable<Assessment>> GetAllAsync();
        Task<Assessment?> GetByIdAsync(int id);
        Task<Assessment> CreateAsync(Assessment assessment);
        Task<bool> UpdateAsync(Assessment assessment);
        Task<bool> DeleteAsync(int id);
    }
}
