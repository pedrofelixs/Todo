using Todo.Core.Models;

namespace Todo.Api.Repositories
{
    public interface IUserRepository
    {
        Task<User> AddUserAsync(User user);
        Task<User?> GetUserByIdAsync(int id);
        Task<IEnumerable<User>> GetAllUsersAsync();
        Task<User?> UpdateUserAsync(User user);
        Task<bool> DeleteUserAsync(int id);
    }
}
