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
        private readonly ILogger<TodoItemController> _logger;

        public TodoItemController(ITodoItemService todoItemService, ILogger<TodoItemController> logger)
        {
            _todoItemService = todoItemService;
            _logger = logger;
        }

        [HttpPost]
        public async Task<ActionResult<Response<TodoItem>>> CreateTodoItem([FromBody] CreateTodoItemRequest request)
        {
            _logger.LogInformation("CreateTodoItem endpoint called with request: {@Request}", request);

            try
            {
                var result = await _todoItemService.CreateAsync(request);
                if (result.IsSuccess)
                {
                    _logger.LogInformation("Todo item created successfully: {@Result}", result);
                    return StatusCode(201, result);
                }
                _logger.LogWarning("Failed to create todo item: {@Result}", result);
                return StatusCode(500, result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while creating a todo item.");
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        [HttpPut]
        public async Task<ActionResult<Response<TodoItem>>> UpdateTodoItem([FromBody] UpdateTodoItemRequest request)
        {
            _logger.LogInformation("UpdateTodoItem endpoint called with request: {@Request}", request);

            try
            {
                var result = await _todoItemService.UpdateAsync(request);
                if (result.IsSuccess)
                {
                    _logger.LogInformation("Todo item updated successfully: {@Result}", result);
                    return Ok(result);
                }
                _logger.LogWarning("Failed to update todo item: {@Result}", result);
                return StatusCode(500, result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while updating a todo item.");
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Response<TodoItem>>> DeleteTodoItem([FromRoute] int id, [FromBody] DeleteTodoItemRequest request)
        {
            _logger.LogInformation("DeleteTodoItem endpoint called with id: {Id} and request: {@Request}", id, request);

            try
            {
                var result = await _todoItemService.DeleteAsync(request);
                if (result.IsSuccess)
                {
                    _logger.LogInformation("Todo item deleted successfully: {@Result}", result);
                    return Ok(result);
                }
                _logger.LogWarning("Failed to delete todo item: {@Result}", result);
                return StatusCode(500, result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while deleting a todo item.");
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Response<TodoItem>>> GetById([FromRoute] int id, [FromQuery] GetTodoItemByIdRequest request)
        {
            _logger.LogInformation("GetById endpoint called with id: {Id} and request: {@Request}", id, request);

            try
            {
                var result = await _todoItemService.GetByIdAsync(request);
                if (result.IsSuccess)
                {
                    _logger.LogInformation("Retrieved todo item by id successfully: {@Result}", result);
                    return Ok(result);
                }
                _logger.LogWarning("Failed to retrieve todo item by id: {@Result}", result);
                return StatusCode(500, result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving a todo item by id.");
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        [HttpGet]
        public async Task<ActionResult<PagedResponse<List<TodoItem>>>> GetAll([FromQuery] GetAllTodoItemsRequest request)
        {
            _logger.LogInformation("GetAll endpoint called with request: {@Request}", request);

            try
            {
                var result = await _todoItemService.GetAllAsync(request);
                if (result.IsSuccess)
                {
                    _logger.LogInformation("Retrieved all todo items successfully: {@Result}", result);
                    return Ok(result);
                }
                _logger.LogWarning("Failed to retrieve all todo items: {@Result}", result);
                return StatusCode(500, result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving all todo items.");
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }
    }
}
