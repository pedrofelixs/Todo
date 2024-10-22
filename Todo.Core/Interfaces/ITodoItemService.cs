using Todo.Api.DTOs.Requests.TodoItems;
using Todo.Api.DTOs.Responses;
using Todo.Core.DTOs.Requests.TodoItems;
using Todo.Core.Models;

namespace Todo.Core.Interfaces
{
    public interface ITodoItemService
    {
        Task<Response<TodoItem?>> CreateAsync(CreateTodoItemRequest request);
        Task<Response<TodoItem?>> UpdateAsync(UpdateTodoItemRequest request);
        Task<Response<TodoItem?>> DeleteAsync(DeleteTodoItemRequest request);
        Task<Response<TodoItem?>> GetByIdAsync(GetTodoItemByIdRequest request);
        Task<PagedResponse<List<TodoItem>>> GetAllAsync(GetAllTodoItemsRequest request);
    }
}
