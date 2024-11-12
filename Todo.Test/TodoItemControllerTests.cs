using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;
using Todo.Api.Controllers;
using Todo.Api.DTOs.Requests.TodoItems;
using Todo.Core.Interfaces;
using Todo.Core.Models;
using Todo.Api.DTOs.Responses;
using Todo.Core.DTOs.Requests.TodoItems;

namespace Todo.Test.Controllers
{
    public class TodoItemControllerTests
    {
        private readonly Mock<ITodoItemService> _todoItemServiceMock;
        private readonly TodoItemController _controller;

        public TodoItemControllerTests()
        {
            _todoItemServiceMock = new Mock<ITodoItemService>();
            //_controller = new TodoItemController(_todoItemServiceMock.Object);
        }

        [Fact]
        public async Task CreateTodoItem_Returns201_WhenSuccess()
        {
            // Arrange
            var request = new CreateTodoItemRequest { /* preencher com dados necessários */ };
            var response = new Response<TodoItem>();
            _todoItemServiceMock.Setup(s => s.CreateAsync(request)).ReturnsAsync(response);

            // Act
            var result = await _controller.CreateTodoItem(request);

            // Assert
            var statusCodeResult = result.Result as ObjectResult;
            Xunit.Assert.Equal(201, statusCodeResult?.StatusCode);
        }

        [Fact]
        public async Task CreateTodoItem_Returns500_WhenFailure()
        {
            // Arrange
            var request = new CreateTodoItemRequest { /* preencher com dados necessários */ };
            var response = new Response<TodoItem>();
            _todoItemServiceMock.Setup(s => s.CreateAsync(request)).ReturnsAsync(response);

            // Act
            var result = await _controller.CreateTodoItem(request);

            // Assert
            var statusCodeResult = result.Result as ObjectResult;
            Xunit.Assert.Equal(500, statusCodeResult?.StatusCode);
        }

        [Fact]
        public async Task UpdateTodoItem_Returns200_WhenSuccess()
        {
            // Arrange
            var request = new UpdateTodoItemRequest { /* preencher com dados necessários */ };
            var response = new Response<TodoItem> ();
            _todoItemServiceMock.Setup(s => s.UpdateAsync(request)).ReturnsAsync(response);

            // Act
            var result = await _controller.UpdateTodoItem(request);

            // Assert
            var statusCodeResult = result.Result as ObjectResult;
            Xunit.Assert.Equal(200, statusCodeResult?.StatusCode);
        }

        [Fact]
        public async Task DeleteTodoItem_Returns200_WhenSuccess()
        {
            // Arrange
            var id = 1;
            var request = new DeleteTodoItemRequest { /* preencher com dados necessários */ };
            var response = new Response<TodoItem> ();
            _todoItemServiceMock.Setup(s => s.DeleteAsync(request)).ReturnsAsync(response);

            // Act
            var result = await _controller.DeleteTodoItem(id, request);

            // Assert
            var statusCodeResult = result.Result as ObjectResult;
            Xunit.Assert.Equal(200, statusCodeResult?.StatusCode);
        }

        [Fact]
        public async Task GetById_Returns200_WhenSuccess()
        {
            // Arrange
            var id = 1;
            var request = new GetTodoItemByIdRequest { /* preencher com dados necessários */ };
            var response = new Response<TodoItem> ();
            _todoItemServiceMock.Setup(s => s.GetByIdAsync(request)).ReturnsAsync(response);

            // Act
            var result = await _controller.GetById(id, request);

            // Assert
            var statusCodeResult = result.Result as ObjectResult;
            Xunit.Assert.Equal(200, statusCodeResult?.StatusCode);
        }
    }
}
