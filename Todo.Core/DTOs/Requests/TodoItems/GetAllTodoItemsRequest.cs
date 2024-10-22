using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Todo.Api.DTOs.Requests;

namespace Todo.Core.DTOs.Requests.TodoItems
{
    public class GetAllTodoItemsRequest : PagedRequest
    {
        public int RequestUserId { get; set; }
        public string? SearchTerm { get; set; }
        public string? SortOrder { get; set; }
    }
}
