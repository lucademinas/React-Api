using Application.Interfaces;
using Application.Models;
using Microsoft.AspNetCore.Authorization;
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

        [Authorize(Policy = "SysAdmin")]
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_userService.Get());
        }

        [Authorize]
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var user = _userService.Get(id);
            if (user == null)
            {
                return NotFound($"No se encontró al usuario con ID: {id}");
            }
            return Ok(user);
        }

        [HttpPost]
        public IActionResult CreateUser([FromBody] UserDto user)
        {
            try
            {
                // Aquí llamas a tu servicio para agregar el usuario
                _userService.Add(user);

                // Retornar respuesta de éxito
                return Ok(new { message = "El usuario fue agregado correctamente" });
            }
            catch (Exception ex)
            {
                // Manejar el error (opcionalmente loguear)
                return BadRequest(new { message = "Error al agregar el usuario: " + ex.Message });
            }
        }
        

        [Authorize]
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
