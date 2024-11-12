using Todo.Core.DTOs.Requests.Users;
using Todo.Core.Models;

namespace Todo.Core.Interfaces
{
    public interface IUserService
    {
        Task<User> CreateUser(CreateUserRequest request);
        Task<User?> UpdateUser(int id, UpdateUserRequest request);
        Task<bool> DeleteUser(int id);
        Task<IEnumerable<User>> GetAllUsers();
    }
}
