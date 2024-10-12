using Application.Interfaces;
using Application.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public IActionResult Get()
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

        [HttpPost]
        public IActionResult CreateProduct([FromBody] ProductDto productDto)
        {
            _productService.Add(productDto);
            return Ok("El producto fue agregado correctamente");
        }

        [HttpPut("Update/{id}")]
        public IActionResult UpdateProduct(int id, [FromBody] ProductDto productDto)
        {
            _productService.Update(id, productDto);
            return Ok("El producto fue actualizado correctamente");
        }

        [HttpDelete]
        public IActionResult DeleteProduct(int id)
        {
            var product = _productService.Get(id);
            if(product == null)
            {
                return NotFound();
            }
            _productService.Delete(id);
            return Ok("El producto fue eliminado");
        }
    }
}
