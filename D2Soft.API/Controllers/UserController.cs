using D2Soft.Application.DTOs;
using D2Soft.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace D2Soft.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserDto>>> Get()
        {
            var users = await _userService.GetAllUsersAsync();
            return Ok(users);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserDto>> Get(int id)
        {
            var user = await _userService.GetUserByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] UserDto userDto)
        {
           var newuser =  await _userService.AddUserAsync(userDto);
            return CreatedAtAction(nameof(Get), new { id = newuser.UserId }, newuser);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] UserDto userDto)
        {
            if (id != userDto.UserId)
            {
                return BadRequest();
            }

            await _userService.UpdateUserAsync(userDto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await _userService.DeleteUserAsync(id);
            return NoContent();
        }
    }
}