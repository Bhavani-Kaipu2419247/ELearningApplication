using ELearningApplication.API.Data;
using ELearningApplication.API.Models;
using Microsoft.EntityFrameworkCore;

namespace ELearningApplication.API.Repositories
{
    public class SubmissionRepository : ISubmissionRepository
    {
        private readonly ApplicationDbContext _context;

        public SubmissionRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Submission>> GetAllAsync()
        {
            return await _context.Submissions.ToListAsync();
        }

        public async Task<Submission?> GetByIdAsync(int id)
        {
            return await _context.Submissions.FindAsync(id);
        }

        public async Task<Submission> CreateAsync(Submission submission)
        {
            _context.Submissions.Add(submission);
            await _context.SaveChangesAsync();
            return submission;
        }

        public async Task<bool> UpdateAsync(Submission submission)
        {
            _context.Entry(submission).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var submission = await _context.Submissions.FindAsync(id);
            if (submission == null) return false;

            _context.Submissions.Remove(submission);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
