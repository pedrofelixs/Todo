namespace Todo.Api.DTOs.Requests.TodoItems
{
    public class UpdateTodoItemRequest : Request
    {
        public int UserId { get; set; }
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        public bool IsCompleted { get; set; }
    }
}
