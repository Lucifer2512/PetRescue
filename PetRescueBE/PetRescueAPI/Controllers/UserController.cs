using BusinessLayer.IServices;
using BusinessLayer.Models.Request;
using Microsoft.AspNetCore.Mvc;

namespace PetRescueAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestModel request)
        {
            var response = await _userService.LoginAsync(request);
            return StatusCode((int)response.Code, response);
        }

        [HttpPost]
        public async Task<IActionResult> AddUser([FromBody] UserRequestModel request)
        {
            var response = await _userService.AddAsync(request);
            return StatusCode((int)response.Code, response);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(Guid id, [FromBody] UserRequestModelForUpdate request)
        {
            var response = await _userService.UpdateAsync(request, id);
            return StatusCode((int)response.Code, response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserDetail(Guid id)
        {
            var response = await _userService.GetDetailAsync(id);
            return StatusCode((int)response.Code, response);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            var response = await _userService.GetAllUsersAsync();
            return StatusCode((int)response.Code, response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(Guid id)
        {
            var response = await _userService.DeleteAsync(id);
            return StatusCode((int)response.Code, response);
        }

        [HttpGet("roles")]
        public async Task<IActionResult> GetAllRoles()
        {
            var response = await _userService.GetAllRoleAsync();
            return StatusCode((int)response.Code, response);
        }

        [HttpPost("role")]
        public async Task<IActionResult> AddRole([FromBody] string role)
        {
            var response = await _userService.AddRoleAsync(role);
            return StatusCode((int)response.Code, response);
        }
    }
}
