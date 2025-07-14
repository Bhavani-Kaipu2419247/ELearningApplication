using ELearningApplication.API.Data;
using ELearningApplication.API.Models;
using Microsoft.EntityFrameworkCore;

namespace ELearningApplication.API.Repositories
{
    public class AssessmentRepository : IAssessmentRepository
    {
        private readonly ApplicationDbContext _context;

        public AssessmentRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Assessment>> GetAllAsync()
        {
            return await _context.Assessments.ToListAsync();
        }

        public async Task<Assessment?> GetByIdAsync(int id)
        {
            return await _context.Assessments.FindAsync(id);
        }

        public async Task<Assessment> CreateAsync(Assessment assessment)
        {
            _context.Assessments.Add(assessment);
            await _context.SaveChangesAsync();
            return assessment;
        }

        public async Task<bool> UpdateAsync(Assessment assessment)
        {
            _context.Entry(assessment).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _context.Assessments.FindAsync(id);
            if (entity == null) return false;

            _context.Assessments.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
