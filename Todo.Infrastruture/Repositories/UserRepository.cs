using Microsoft.EntityFrameworkCore;
using Todo.Api.Data;
using Todo.Core.Models;

namespace Todo.Api.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly TodoDataContext _context;

        public UserRepository(TodoDataContext context)
        {
            _context = context;
        }

        // Adicionar um novo usuário
        public async Task<User> AddUserAsync(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return user;
        }

        // Buscar um usuário pelo ID
        public async Task<User?> GetUserByIdAsync(int id)
        {
            return await _context.Users
                .Include(u => u.TodoItems) // Inclui as tarefas associadas
                .FirstOrDefaultAsync(u => u.Id == id);
        }

        // Listar todos os usuários
        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            return await _context.Users.ToListAsync();
        }

        // Atualizar um usuário existente
        public async Task<User?> UpdateUserAsync(User user)
        {
            var existingUser = await _context.Users.FindAsync(user.Id);
            if (existingUser == null)
                return null;

            existingUser.Name = user.Name;
            existingUser.Email = user.Email;
            existingUser.PasswordHash = user.PasswordHash;
            existingUser.LastLogin = user.LastLogin;

            await _context.SaveChangesAsync();
            return existingUser;
        }

        // Deletar um usuário e suas tarefas vinculadas
        public async Task<bool> DeleteUserAsync(int id)
        {
            var user = await _context.Users.Include(u => u.TodoItems).FirstOrDefaultAsync(u => u.Id == id);

            if (user == null)
                return false;

            // Remove as tarefas do usuário
            _context.TodoItems.RemoveRange(user.TodoItems);

            // Remove o próprio usuário
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
