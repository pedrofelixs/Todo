using Todo.Api.Data;
using Todo.Api.DTOs.Requests;
using Todo.Core.Models;
using Todo.Core.Repositories;
using Microsoft.EntityFrameworkCore;
using Todo.Core.DTOs.Requests.TodoItems;


namespace Todo.Infrastruture.Repositories
{
    public class TodoItemRepository : ITodoItemRepository
    {
        private readonly TodoDataContext _context;

        public TodoItemRepository(TodoDataContext context)
        {
            _context = context;
        }

        public async Task<TodoItem?> GetByIdAsync(int id, int userId)
        {
            return await _context.TodoItems.FindAsync(id);
        }

        public async Task<TodoItem?> GetByIdAsync(int id)
        {
            return await _context.TodoItems.FindAsync(id);
        }

        public async Task<IEnumerable<TodoItem>> GetAllAsync()
        {
            return await _context.TodoItems
                                 .Include(t => t.User) // Eager Loading do usuário relacionado
                                 .ToListAsync();
        }

        public async Task AddAsync(TodoItem item)
        {
            await _context.TodoItems.AddAsync(item);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(TodoItem item)
        {
            _context.TodoItems.Update(item);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(TodoItem item)
        {
            _context.TodoItems.Remove(item);
            await _context.SaveChangesAsync();
        }

        public async Task<int> CountAsync(int userId)
        {
            return await _context.TodoItems.CountAsync(t => t.UserId == userId);
        }

        public IQueryable<TodoItem> Query(GetAllTodoItemsRequest request)
        {
            return _context.TodoItems
                           .AsNoTracking()
                           .Where(x => x.UserId == request.RequestUserId);
        }

    }
}
