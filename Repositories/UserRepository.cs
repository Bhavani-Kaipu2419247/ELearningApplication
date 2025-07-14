using ELearningApplication.API.Data;
using ELearningApplication.API.Models;
using Microsoft.EntityFrameworkCore;

namespace ELearningApplication.API.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<User> RegisterUserAsync(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public User Authenticate(string email, string password)
        {
            return _context.Users.FirstOrDefault(u => u.Email == email && u.Password == password);
        }

        public async Task<User> GetUserByIdAsync(int id)
        {
            return await _context.Users.FindAsync(id);
        }

        public async Task<bool> UserExistsAsync(string email)
        {
            return await _context.Users.AnyAsync(u => u.Email == email);
        }

        public async Task UpdateUserAsync(User updatedUser)
        {
            //_context.Entry(updatedUser).State = EntityState.Modified;
            _context.Entry(updatedUser).State = EntityState.Modified;
            await _context.SaveChangesAsync();


        }
    }
}

