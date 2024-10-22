using Todo.Api.DTOs.Requests;

namespace Todo.Core.DTOs.Requests.Users
{
    public class GetAllUserRequest : PagedRequest
    {
        public string? SearchTerm { get; set; }
        public string? SortOrder { get; set; }
    }
}
