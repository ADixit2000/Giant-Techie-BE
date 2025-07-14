using Giant_Techie_BE.DTOs;
using Giant_Techie_BE.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Giant_Techie_BE.Controller
{
    [Route("User")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        [Route("Create")]
        [HttpPost]

        public async Task<IActionResult> CreateUser([FromBody] AddOrUpdateUser command)
        {
            var user = await _userService.CreateUsers(command);
            return CreatedAtAction(nameof(GetUserById), new { id = user.Id }, user);
        }

        [Route("GetById/{id}")]
        [HttpGet]
        public async Task<IActionResult> GetUserById(Guid id)
        {
            var user = await _userService.GetUserById(id);
            if (user == null)
                return NotFound();
            return Ok(user);
        }

        [Route("GetAll")]
        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _userService.GetAllUsers();
            return Ok(users);
        }

        [Route("Update/{id}")]
        [HttpPut]

        public async Task<IActionResult> UpdateUser(Guid id, [FromBody] AddOrUpdateUser command)
        {
            await _userService.UpdateUser(id, command);
            return NoContent();
        }

        [Route("Delete/{id}")]
         [HttpDelete]

        public async Task<IActionResult> DeleteUser(Guid id)
        {
            await _userService.DeleteUser(id);
            return NoContent();
        }

        [Route("login")]
        [HttpGet]
        public async Task<IActionResult> LoginUser(string email, string password)
        {
            var user = await _userService.LoginUser(email, password);
            if (user == null)
                return NotFound();
            return Ok(user);
        }

    }
}
