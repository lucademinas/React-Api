using Application.Interfaces;
using Application.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize(Policy = "SysAdmin")]
    public class SysController : ControllerBase
    {
        private readonly IUserService _userService;

        public SysController(IUserService userService)
        {
            _userService = userService;
        }

      
        [HttpGet]
        public IActionResult GetAllUsers()
        {
            return Ok(_userService.Get());
        }

        [HttpGet("{id}")]
        public IActionResult GetUserById(int id)
        {
            var user = _userService.Get(id);
            if (user == null)
            {
                return NotFound($"No se encontró al usuario con ID: {id}");
            }
            return Ok(user);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateUser([FromBody] UserUpdateDTO user, [FromRoute]int id)
        {
            _userService.Update(user, id);
            return Ok("El usuario fue actualizado correctamente");
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteUser([FromRoute]int id)
        {
            var user = _userService.Get(id);
            if (user == null)
            {
                return NotFound($"No se encontró al usuario con ID: {id}");
            }
            _userService.Delete(id);
            return Ok("El usuario fue eliminado correctamente");
        }
    }
}
