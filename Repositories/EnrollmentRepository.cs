using ELearningApplication.API.Data;
using ELearningApplication.API.Models;
using Microsoft.EntityFrameworkCore;

namespace ELearningApplication.API.Repositories
{
        public class EnrollmentRepository : IEnrollmentRepository
        {
            private readonly ApplicationDbContext _context;

            public EnrollmentRepository(ApplicationDbContext context)
            {
                _context = context;
            }

            public async Task<Enrollment?> GetEnrollmentByIdAsync(int id)
            {
                return await _context.Enrollments.FindAsync(id);
            }

            public async Task<IEnumerable<Enrollment>> GetEnrollmentsByStudentAsync(int studentId)
            {
                return await _context.Enrollments
                    .Where(e => e.StudentId == studentId)
                    .ToListAsync();
            }

            public async Task<Enrollment> AddEnrollmentAsync(Enrollment enrollment)
            {
                _context.Enrollments.Add(enrollment);
                await _context.SaveChangesAsync();
                return enrollment;
            }

            public async Task<bool> EnrollmentExistsAsync(int studentId, int courseId)
            {
                return await _context.Enrollments
                    .AnyAsync(e => e.StudentId == studentId && e.CourseId == courseId);
            }

            public async Task<bool> UpdateEnrollmentAsync(Enrollment enrollment)
            {
                _context.Entry(enrollment).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return true;
            }

            public async Task<bool> DeleteEnrollmentAsync(int id)
            {
                var enrollment = await _context.Enrollments.FindAsync(id);
                if (enrollment == null) return false;

                _context.Enrollments.Remove(enrollment);
                await _context.SaveChangesAsync();
                return true;
            }



        }
}
