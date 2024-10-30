using Application.Interfaces;
using Application.Models;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IAdminService _adminService;

        public AdminController(IAdminService adminService)
        {
            _adminService = adminService;
        }

        [HttpGet]
        public IActionResult GetAllAdmins()
        {
            return Ok(_adminService.Get());
        }

        [HttpGet("{id}")]
        public IActionResult GetAdminById(int id)
        {
            return Ok(_adminService.Get(id));
        }

        [HttpPost]
        public IActionResult CreateAdmin([FromBody] UserCreateDTO adminDto)
        {
            _adminService.Add(adminDto);
            return Ok("El admin fue agregado correctamente");
        }

        [HttpPut("{id}")]
        public IActionResult UpdateAdmin(int id, [FromBody] UserUpdateDTO adminDto)
        {
            var client = _adminService.Get(id);
            if (client == null)
            {
                return NotFound($"No se encontró ningún admin con el ID: {id}");
            }
            _adminService.Update(id, adminDto);
            return Ok("El admin fue actualizado correctamente");
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteAdmin(int id)
        {
            var client = _adminService.Get(id);
            if (client == null)
            {
                return NotFound($"No se encontró ningún admin con el ID: {id}");
            }
            _adminService.Delete(id);
            return Ok("El admin fue eliminado correctamente");

        }
    }
}
