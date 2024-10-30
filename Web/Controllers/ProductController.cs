using Application.Interfaces;
using Application.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Web.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        private int? GetAdminId()
        {
            var adminIdClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
            if (adminIdClaim != null && int.TryParse(adminIdClaim.Value, out var adminId))
            {
                return adminId;
            }
            return null;
        }

        [HttpGet]
        public IActionResult GetAllProducts()
        {
            return Ok(_productService.Get());
        }

        [HttpGet("{id}")]
        public IActionResult GetProductById(int id)
        {
            var product = _productService.Get(id);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }

        [Authorize(Policy = "Admin")]
        [HttpGet("{adminId}")]
        public IActionResult GetProductsByAdmin(int adminId)
        {
            var products = _productService.GetAllByAdmin(adminId);
            if (products == null || !products.Any())
            {
                return NotFound("No se encontraron productos para este administrador.");
            }
            return Ok(products);
        }

        [Authorize(Policy = "Admin")]
        [HttpPost]
        public IActionResult CreateProduct([FromBody] ProductDto productDto)
        {
            var adminId = GetAdminId();
            if (adminId == null)
            {
                return Forbid();
            }
            _productService.Add(adminId.Value, productDto);
            return Ok("El producto fue agregado correctamente");
        }

        [Authorize(Policy = "Admin")]
        [HttpPut("{id}")]
        public IActionResult UpdateProduct(int id, [FromBody] ProductDto productDto)
        {
            var adminId = GetAdminId();
            if(adminId == null)
            {
                return Forbid();
            }

            var existingProduct = _productService.Get(id);
            if (existingProduct == null)
            {
                return NotFound($"No se encontró el producto con el ID: {id}");
            }

            if (existingProduct.AdminId != adminId.Value)
            {
                return Forbid(); // Prohibir si el admin no es el propietario del producto
            }

            _productService.Update(id, productDto);
            return Ok("El producto fue actualizado correctamente");
        }

        [Authorize(Policy = "Admin")]
        [HttpDelete("{id}")]
        public IActionResult DeleteProduct(int id)
        {
            var adminId = GetAdminId();
            if (adminId == null)
            {
                return Forbid();
            }

            var existingProduct = _productService.Get(id);
            if (existingProduct == null)
            {
                return NotFound($"No se encontró el producto con el ID: {id}");
            }

            if (existingProduct.AdminId != adminId.Value)
            {
                return Forbid(); // Prohibir si el admin no es el propietario del producto
            }
            _productService.Delete(id);
            return Ok("El producto fue eliminado");
        }
    }
}
