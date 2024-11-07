using Application.Interfaces;
using Application.Models;
using Application.Services;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace TPI_Ecommerce.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class OrderDetailController : ControllerBase
    {
        private readonly IOrderDetailService _orderDetailService;
        private readonly IOrderService _orderService;
        private readonly IProductService _productService;


        public OrderDetailController(IOrderDetailService orderDetailService, IOrderService orderService, IProductService productService)
        {
            _orderDetailService = orderDetailService;
            _orderService = orderService;
            _productService = productService;
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

        [HttpGet("{saleOrderId}")]
        public IActionResult GetAllBySaleOrder(int saleOrderId)
        {
            var userId = GetUserId();
            if (userId == null)
            {
                return Forbid();
            }

            var saleOrder = _orderService.Get(saleOrderId);
            if (saleOrder is null)
            {
                return NotFound($"No se encontro ninguna venta con el ID: {saleOrderId}");
            }

            
             var saleOrderDetails = _orderDetailService.GetAllBySaleOrder(saleOrderId);
             return Ok(saleOrderDetails);

        }

        [HttpGet("{productId}")]
        public IActionResult GetAllByProduct(int productId)
        {
            var product = _productService.Get(productId);
            if (product is null)
            {
                return NotFound($"No se encontro el producto con el ID: {productId}");
            }

            var saleOrderDetails = _orderDetailService.GetAllByProducts(productId);
            return Ok(saleOrderDetails);           
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var userId = GetUserId();
            if (userId == null)
            {
                return Forbid();
            }

            var saleOrderDetail = _orderDetailService.Get(id);
            if (saleOrderDetail is null)
            {
                return NotFound($"No se encontro la linea de venta con el ID: {id}");
            }
            
            return Ok(saleOrderDetail);
        }

        [Authorize(Policy = "Client")]
        [HttpPost]
        public IActionResult AddSaleOrderDetail([FromBody] OrderDetailCreateDto dto)
        {
            var userId = GetUserId();
            if (userId == null)
            {
                return Forbid();
            }

            var actualSaleOrder = _orderService.Get(dto.SaleOrderId);
            if (actualSaleOrder is null)
            {
                return NotFound($"No se encontro la venta con el ID {dto.SaleOrderId}");
            }

            var productSelected = _productService.Get(dto.ProductId);
            if (productSelected is null)
            {
                return NotFound($"No se encontro el producto con el ID {dto.ProductId}");
            }


            _productService.UpdateStock(productSelected.Id, new ProductUpdateDto()
            {
                 Price = productSelected.Price,
                 Stock = productSelected.Stock - dto.Amount,
            });

            _orderDetailService.Add(dto);
            return Ok("La linea de venta fue agregada");

        }
    
        [HttpDelete("{id}")]
        public IActionResult DeleteSaleOrderDetail(int id, int saleOrderId)
        {
            var userId = GetUserId();
            if (userId == null)
            {
                return Forbid();
            }

            var actualSaleOrder = _orderService.Get(saleOrderId);
            if (actualSaleOrder == null)
            {
                return NotFound($"No se encontró ninguna venta con el ID: {saleOrderId}");
            }

            
             var actualSaleOrderDetail = _orderDetailService.Get(id);
             if (actualSaleOrderDetail is null)
             {
                 return NotFound($"No se encontró la la linea de venta con el ID: {id}");

             }
             _orderDetailService.Delete(id);
             return Ok($"La linea de venta con ID {id} fue eliminada");
           
        }
    }

}

