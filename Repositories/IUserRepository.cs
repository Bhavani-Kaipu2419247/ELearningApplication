using ELearningApplication.API.Models;

namespace ELearningApplication.API.Repositories
{
    public interface IUserRepository
    {
        Task<User> RegisterUserAsync(User user);
        User Authenticate(string email, string password);
        Task<User> GetUserByIdAsync(int id);
        Task<bool> UserExistsAsync(string email);
        Task UpdateUserAsync(User updatedUser);
    }
}
