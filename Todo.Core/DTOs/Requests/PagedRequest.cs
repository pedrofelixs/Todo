using Todo.Core;

namespace Todo.Api.DTOs.Requests
{
    public class PagedRequest : Request
    {
        public int PageNumber { get; set; } = Configuration.DefaultPageNumber;
        public int PageSize { get; set; } = Configuration.DefaultPageSize;
    }
}
