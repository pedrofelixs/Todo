using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Todo.Api.DTOs.Requests;

namespace Todo.Core.DTOs.Requests.TodoItems
{
    public class DeleteTodoItemRequest : Request
    {
        public int UserId { get; set; }
        public int Id { get; set; }
    }
}
