using Todo.Core.DTOs.Requests.TodoItems;
using Todo.Core.Models;

namespace Todo.Core.Repositories
{
    public interface ITodoItemRepository
    {
        Task<TodoItem> GetByIdAsync(int id, int UserId);
        Task<TodoItem?> GetByIdAsync(int id);
        Task<IEnumerable<TodoItem>> GetAllAsync();
        Task AddAsync(TodoItem item);
        Task UpdateAsync(TodoItem item);
        Task DeleteAsync(TodoItem item);
        Task<int> CountAsync(int userId);
        IQueryable<TodoItem> Query(GetAllTodoItemsRequest request);
    }
}
