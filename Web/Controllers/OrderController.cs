using Application.Interfaces;
using Application.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace TPI_Ecommerce.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;
        private readonly IClientService _clientService;

        public OrderController(IOrderService saleOrderService, IClientService clientService)
        {
            _orderService = saleOrderService;
            _clientService = clientService;
        }

        private int? GetUserId() //Funcion para obtener el userId de las claims del usuario autenticado en el contexto de la solicitud actual.
        {
            var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
            if (userIdClaim != null && int.TryParse(userIdClaim.Value, out var userId))
            {
                return userId;
            }
            return null;
        }

        [HttpGet("{id}")]
        public IActionResult GetById([FromRoute] int id)
        {
            
              var saleOrder = _orderService.Get(id);
              if (saleOrder is null)
              {
                    return NotFound($"No se encontró la venta con ID: {id}");
              }
              return Ok(saleOrder);
            
        }

        [HttpGet("{clientId}")]
        public IActionResult GetAllByClient([FromRoute] int clientId)
        {
            var userId = GetUserId();
            if (userId == null)
            {
                return Forbid();
            }

            var client = _clientService.Get(clientId);
            if (client is null)
            {
                return NotFound($"No se encontró al cliente con ID: {clientId}");
            }

            var saleOrders = _orderService.GetAllByClient(clientId);

            if (saleOrders.Count == 0)
            {
               return BadRequest($"El cliente con ID: {clientId} todavía no hizo ninguna compra");

            }
            return Ok(saleOrders);
            
        }

        [Authorize(Policy = "Client")]
        [HttpPost]
        public IActionResult AddSaleOrder([FromBody] OrderDto saleOrderCreate)
        {
            var userId = GetUserId();
            if (userId == null)
            {
                return Forbid();
            }

            var client = _clientService.Get(saleOrderCreate.ClientId);
            if (client is null)
            {
                return NotFound($"No se encontró al cliente con ID: {saleOrderCreate.ClientId}");
            }

       
             _orderService.Add(saleOrderCreate);
             return Ok($"Creada la venta para el cliente ID: {saleOrderCreate.ClientId}");
            
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteSaleOrder(int id)
        {
            var userId = GetUserId();
            if (userId == null)
            {
                return Forbid();
            }
            var saleOrder = _orderService.Get(id);
            if (saleOrder is null)
                return NotFound($"No se encontró la venta con ID: {id}");
            
            _orderService.Delete(id);
            return Ok($"Venta con ID: {id} eliminada");           
        }

        [Authorize(Policy = "Admin")]
        [HttpGet("{adminId}")]
        public IActionResult GetOrdersByAdmin([FromRoute] int adminId)
        {
            // Verifica que el usuario que hace la solicitud sea el administrador que se está buscando o un superadministrador

            var orders = _orderService.GetOrdersByAdmin(adminId);
            if (orders == null || orders.Count == 0)
            {
                return NotFound($"No se encontraron órdenes asociadas al administrador con ID: {adminId}");
            }

            return Ok(orders);
        }
    }
}

