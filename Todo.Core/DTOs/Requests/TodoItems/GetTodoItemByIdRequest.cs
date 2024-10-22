using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Todo.Core.DTOs.Requests.TodoItems
{
    public class GetTodoItemByIdRequest
    {
        public int UserId { get; set; }
        public int Id { get; set; }
    }
}
