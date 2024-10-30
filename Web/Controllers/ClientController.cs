using Application.Interfaces;
using Application.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private readonly IClientService _clientService;

        public ClientController(IClientService clientService)
        {
            _clientService = clientService;
        }

        [HttpGet]
        public IActionResult GetAllClients()
        {
            return Ok(_clientService.Get());
        }

        [HttpGet("{id}")]
        public IActionResult GetClientById(int id)
        {
            return Ok(_clientService.Get(id));
        }

        [HttpPost]
        public IActionResult CreateClient([FromBody]UserCreateDTO clientDto)
        {
            _clientService.Add(clientDto);
            return Ok("El cliente fue agregado correctamente");
        }

        [HttpPut("{id}")]
        public IActionResult UpdateClient(int id, [FromBody]UserUpdateDTO clientDto)
        {
            var client = _clientService.Get(id);
            if (client == null)
            {
                return NotFound($"No se encontró ningún cliente con el ID: {id}");
            }
            _clientService.Update(id, clientDto);
            return Ok("El cliente fue actualizado correctamente");
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteClient(int id)
        {
            var client = _clientService.Get(id);
            if (client == null)
            {
                return NotFound($"No se encontró ningún cliente con el ID: {id}");
            }
            _clientService.Delete(id);
            return Ok("El cliente fue eliminado correctamente");

        }
    }
}
