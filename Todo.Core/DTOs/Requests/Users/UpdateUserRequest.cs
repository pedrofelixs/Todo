namespace Todo.Core.DTOs.Requests.Users
{
    public class UpdateUserRequest
    {
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public DateTime LastLogin { get; set; }
    }
}
