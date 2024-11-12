using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Todo.Api.Controllers;
using Todo.Core.DTOs.Requests.Users;
using Todo.Core.Interfaces;
using Todo.Core.Models;
using Xunit;

namespace Todo.Test.Controllers
{
    public class UserControllerTests
    {
        private readonly Mock<IUserService> _userServiceMock;
        private readonly Mock<ILogger<UserController>> _loggerMock;
        private readonly UserController _controller;

        public UserControllerTests()
        {
            _userServiceMock = new Mock<IUserService>();
            _loggerMock = new Mock<ILogger<UserController>>();
            _controller = new UserController(_userServiceMock.Object, _loggerMock.Object);
        }

        [Fact]
        public async Task CreateUser_Returns201_WhenSuccessful()
        {
            // Arrange
            var request = new CreateUserRequest { /* Preencha com os dados necessários */ };
            var user = new User { Id = 1, Name = "John Doe" }; // Exemplo de instância da classe User

            _userServiceMock.Setup(service => service.CreateUser(It.IsAny<CreateUserRequest>()))
                .ReturnsAsync(user);

            // Act
            var result = await _controller.CreateUser(request);

            // Assert
            var createdAtResult = Xunit.Assert.IsType<CreatedAtActionResult>(result);
            Xunit.Assert.Equal(201, createdAtResult.StatusCode);
            Xunit.Assert.Equal(user, createdAtResult.Value);
        }

        [Fact]
        public async Task CreateUser_Returns500_WhenExceptionThrown()
        {
            // Arrange
            var request = new CreateUserRequest { /* Preencha com os dados necessários */ };

            _userServiceMock.Setup(service => service.CreateUser(It.IsAny<CreateUserRequest>()))
                .ThrowsAsync(new Exception("Erro ao criar usuário"));

            // Act
            var result = await _controller.CreateUser(request);

            // Assert
            var objectResult = Xunit.Assert.IsType<ObjectResult>(result);
            Xunit.Assert.Equal(500, objectResult.StatusCode);
        }

        [Fact]
        public async Task UpdateUser_Returns200_WhenSuccessful()
        {
            // Arrange
            var request = new UpdateUserRequest { /* Preencha com os dados necessários */ };
            var user = new User { Id = 1, Name = "John Doe" };

            _userServiceMock.Setup(service => service.UpdateUser(It.IsAny<int>(), It.IsAny<UpdateUserRequest>()))
                .ReturnsAsync(user);

            // Act
            var result = await _controller.UpdateUser(1, request);

            // Assert
            var okResult = Xunit.Assert.IsType<OkObjectResult>(result);
            Xunit.Assert.Equal(200, okResult.StatusCode);
            Xunit.Assert.Equal(user, okResult.Value);
        }

        [Fact]
        public async Task UpdateUser_Returns404_WhenUserNotFound()
        {
            // Arrange
            var request = new UpdateUserRequest { /* Preencha com os dados necessários */ };

            _userServiceMock.Setup(service => service.UpdateUser(It.IsAny<int>(), It.IsAny<UpdateUserRequest>()))
                .ReturnsAsync((User)null);

            // Act
            var result = await _controller.UpdateUser(1, request);

            // Assert
            Xunit.Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task DeleteUser_Returns204_WhenSuccessful()
        {
            // Arrange
            _userServiceMock.Setup(service => service.DeleteUser(It.IsAny<int>()))
                .ReturnsAsync(true);

            // Act
            var result = await _controller.DeleteUser(1);

            // Assert
            Xunit.Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task DeleteUser_Returns404_WhenUserNotFound()
        {
            // Arrange
            _userServiceMock.Setup(service => service.DeleteUser(It.IsAny<int>()))
                .ReturnsAsync(false);

            // Act
            var result = await _controller.DeleteUser(1);

            // Assert
            Xunit.Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task GetAllUsers_Returns200_WhenSuccessful()
        {
            // Arrange
            var users = new List<User> { new User { Id = 1, Name = "John Doe" } };

            _userServiceMock.Setup(service => service.GetAllUsers())
                .ReturnsAsync(users);

            // Act
            var result = await _controller.GetAllUsers();

            // Assert
            var okResult = Xunit.Assert.IsType<OkObjectResult>(result);
            Xunit.Assert.Equal(200, okResult.StatusCode);
            Xunit.Assert.Equal(users, okResult.Value);
        }

        [Fact]
        public async Task GetAllUsers_Returns500_WhenExceptionThrown()
        {
            // Arrange
            _userServiceMock.Setup(service => service.GetAllUsers())
                .ThrowsAsync(new Exception("Erro ao obter usuários"));

            // Act
            var result = await _controller.GetAllUsers();

            // Assert
            var objectResult = Xunit.Assert.IsType<ObjectResult>(result);
            Xunit.Assert.Equal(500, objectResult.StatusCode);
        }
    }
}
