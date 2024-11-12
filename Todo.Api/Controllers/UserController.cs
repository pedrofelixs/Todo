using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Todo.Api.Services;
using Todo.Core.DTOs.Requests.Users;
using Todo.Core.Interfaces;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Abstractions;

namespace Todo.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ILogger<UserController> _logger;
        private readonly ILogRepository _logRepository;
        public UserController(IUserService userService, ILogger<UserController> logger)
        {
            _userService = userService;
            _logger = logger;
        }

        // Criar um novo usuário
        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] CreateUserRequest request)
        {
            _logger.LogInformation("CreateUser endpoint called with request: {@Request}", request);

            try
            {
                var user = await _userService.CreateUser(request);
                _logger.LogInformation("User created successfully with ID: {UserId}", user.Id);

                return CreatedAtAction(nameof(GetAllUsers), new { id = user.Id }, user);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while creating a user.");
                return StatusCode(500, "An error occurred while processing your request."); 
            }
        }

        // Atualizar um usuário existente
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id, [FromBody] UpdateUserRequest request)
        {
            _logger.LogInformation("UpdateUser endpoint called with ID: {UserId} and request: {@Request}", id, request);

            try
            {
                var user = await _userService.UpdateUser(id, request);

                if (user == null)
                {
                    _logger.LogWarning("User with ID: {UserId} not found for update.", id);
                    return NotFound();
                }

                _logger.LogInformation("User updated successfully with ID: {UserId}", user.Id);
                return Ok(user);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while updating a user with ID: {UserId}", id);
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        // Deletar um usuário
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            _logger.LogInformation("DeleteUser endpoint called with ID: {UserId}", id);

            try
            {
                var deleted = await _userService.DeleteUser(id);

                if (!deleted)
                {
                    _logger.LogWarning("User with ID: {UserId} not found for deletion.", id);
                    return NotFound();
                }

                _logger.LogInformation("User deleted successfully with ID: {UserId}", id);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while deleting a user with ID: {UserId}", id);
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        // Listar todos os usuários
        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            _logger.LogInformation("GetAllUsers endpoint called.");

            try
            {
                var users = await _userService.GetAllUsers();
                _logger.LogInformation("Retrieved all users successfully.");
                return Ok(users);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving users.");
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }
    }
}
