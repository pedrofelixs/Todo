namespace Todo.Api.DTOs.Requests.TodoItems
{
    public class CreateTodoItemRequest : Request
    {
        public int UserId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
    }
}
