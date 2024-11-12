using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System.Threading.Tasks;
using Todo.Api.Controllers;
using Todo.Api.DTOs.Requests.TodoItems;
using Todo.Api.DTOs.Responses;
using Todo.Core.Interfaces;
using Todo.Core.Models;
using Xunit;

namespace Todo.Test.Controllers
{
    public class TodoItemControllerTests
    {
        private readonly Mock<ITodoItemService> _todoItemServiceMock;
        private readonly Mock<ILogger<TodoItemController>> _loggerMock;
        private readonly TodoItemController _controller;

        public TodoItemControllerTests()
        {
            _todoItemServiceMock = new Mock<ITodoItemService>();
            _loggerMock = new Mock<ILogger<TodoItemController>>();
            _controller = new TodoItemController(_todoItemServiceMock.Object, _loggerMock.Object);
        }

        [Fact]
        public async Task CreateTodoItem_Returns201_WhenSuccessful()
        {
            // Arrange
            var request = new CreateTodoItemRequest { /* Preencha com dados necessários */ };
            var response = new Response<TodoItem> {};

            _todoItemServiceMock.Setup(service => service.CreateAsync(request))
                .ReturnsAsync(response);

            // Act
            var result = await _controller.CreateTodoItem(request);

            // Assert
            var statusCodeResult = result.Result as ObjectResult;
            Xunit.Assert.Equal(201, statusCodeResult?.StatusCode);
        }
    }
}
