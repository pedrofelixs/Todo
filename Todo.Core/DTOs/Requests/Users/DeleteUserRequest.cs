using Todo.Core.Models;
namespace Todo.Core.DTOs.Requests.Users
{
    public class DeleteUserRequest
    {
        public List<TodoItem> TodoItems { get; set; }
        public int Id { get; set; }
    }
}
