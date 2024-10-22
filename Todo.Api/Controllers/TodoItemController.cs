using Microsoft.AspNetCore.Mvc;
using Todo.Api.DTOs.Requests.TodoItems;
using Todo.Api.DTOs.Responses;
using Todo.Core.DTOs.Requests.TodoItems;
using Todo.Core.Interfaces;
using Todo.Core.Models;

namespace Todo.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TodoItemController : ControllerBase
    {
        private readonly ITodoItemService _todoItemService;

        public TodoItemController(ITodoItemService todoItemService)
        {
            _todoItemService = todoItemService;
        }

        [HttpPost]
        public async Task<ActionResult<Response<TodoItem>>> CreateTodoItem([FromBody] CreateTodoItemRequest request)
        {
            var result = await _todoItemService.CreateAsync(request);
            return StatusCode(result.IsSuccess ? 201 : 500, result);
        }

        [HttpPut]
        public async Task<ActionResult<Response<TodoItem>>> UpdateTodoItem([FromBody] UpdateTodoItemRequest request)
        {
            var result = await _todoItemService.UpdateAsync(request);
            return StatusCode(result.IsSuccess ? 200 : 500, result);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Response<TodoItem>>> DeleteTodoItem([FromRoute] int id, [FromBody] DeleteTodoItemRequest request)
        {
            var result = await _todoItemService.DeleteAsync(request);
            return StatusCode(result.IsSuccess ? 200 : 500, result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Response<TodoItem>>> GetById([FromRoute] int id, [FromQuery] GetTodoItemByIdRequest request)
        {
            var result = await _todoItemService.GetByIdAsync(request);
            return StatusCode(result.IsSuccess? 200 : 500, result);
        }

        [HttpGet]
        public async Task<ActionResult<PagedResponse<List<TodoItem>>>> GetAll([FromQuery] GetAllTodoItemsRequest request)
        {
            var result = await _todoItemService.GetAllAsync(request);
            return StatusCode(result.IsSuccess ? 200 : 500, result);
        }


    }   
}
