using Application.Interfaces;
using Application.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
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
        public IActionResult Get()
        {
            return Ok(_userService.Get());
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var user = _userService.Get(id);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        [HttpPost]
        public IActionResult CreateUser([FromBody] UserDto user)
        {
            _userService.Add(user);
            return Ok("El usuario fue agregado correctamente");
        }

        [HttpPut]
        public IActionResult UpdateUser([FromBody] UserDto user, int id)
        {
            _userService.Update(user, id);
            return Ok("El usuario fue actualizado correctamente");
        }

        [HttpDelete]
        public IActionResult DeleteUser(int id)
        {
            var user = _userService.Get(id);
            if (user == null)
            {
                return NotFound();
            }
            _userService.Delete(id);
            return Ok("El usuario fue eliminado correctamente");
        }
    }
}
