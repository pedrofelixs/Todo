using Todo.Api.Repositories;
using Todo.Core.Models;
using Todo.Core.DTOs.Requests.Users;
using Todo.Core.Interfaces;

namespace Todo.Api.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<User> CreateUser(CreateUserRequest request)
        {
            var user = new User
            {
                Name = request.Name,
                Email = request.Email,
                PasswordHash = request.PasswordHash,
                CreatedAt = DateTime.UtcNow
            };

            return await _userRepository.AddUserAsync(user);
        }

        public async Task<User?> UpdateUser(int id, UpdateUserRequest request)
        {
            var user = await _userRepository.GetUserByIdAsync(id);

            if (user == null)
                return null;

            user.Name = request.Name;
            user.Email = request.Email;
            user.PasswordHash = request.PasswordHash;
            user.LastLogin = request.LastLogin;

            return await _userRepository.UpdateUserAsync(user);
        }

        public async Task<bool> DeleteUser(int id)
        {
            return await _userRepository.DeleteUserAsync(id);
        }

        public async Task<IEnumerable<User>> GetAllUsers()
        {
            return await _userRepository.GetAllUsersAsync();
        }
    }
}
