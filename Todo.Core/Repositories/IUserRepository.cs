using Todo.Core.DTOs.Requests.Users;
using Todo.Core.Models;

namespace Todo.Core.Repositories
{
    public interface IUserRepository
    {
        Task<User?> GetByIdAsync(int id);
        Task<List<User>> GetAllAsync(int pageNumber, int pageSize);
        Task AddAsync(User User);
        Task UpdateAsync(User user);
        Task DeleteAsync(User user);
        IQueryable<User> Query(GetAllUserRequest request);
    }
}
