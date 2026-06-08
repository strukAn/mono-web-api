using System.Reflection.Metadata.Ecma335;
using ExampleProject.Common;
using ExampleProject.Model;
using ExampleProject.Service;
using Microsoft.AspNetCore.Mvc;

namespace ExampleProject.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductsController : ControllerBase
    {
        [HttpGet("all")]
        public IActionResult GetAll([FromQuery] int stock = -1, [FromQuery] string name = "", [FromQuery] string description = "")
        {
            ProductService service = new ProductService();
            ProductFilter filter = new ProductFilter();

            filter.Name = name;
            filter.Stock = stock;
            filter.Description = description;

            List<Product> products = service.GetAll(filter);

            if(products == null)
            {
                return NotFound("No products found");
            }

            return Ok(products);
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            ProductService service = new ProductService();
            Product product = service.Get(id);

            if(product == null)
            {
                return NotFound("Product not found");
            }

            return Ok(product);
        }

        [HttpDelete("delete")]
        public IActionResult Delete(int id)
        {
            ProductService service = new ProductService();
            int result = service.Delete(id);

            switch (result) {
                case 0:
                    return NotFound("Product not found");
                case -1:
                    return BadRequest("Exception thrown in repository");
                default:
                    return Ok("Product deleted");
            }

        }

        [HttpPost("add")]
        public IActionResult Post(ProductCategoryDTO productDto)
        {
            ProductService service = new ProductService();
            int result = service.Add(productDto);

            switch (result)
            {
                case 0:
                    return BadRequest("Product not added");
                case -1:
                    return BadRequest("Exception thrown in repository");
                default:
                    return Ok("Product deleted");
            }
        }

        [HttpPut("update")]
        public IActionResult Put(int id, ProductCategoryDTO updated)
        {
            ProductService service = new ProductService();
            int result = service.Update(id, updated);

            switch (result)
            {
                case 0:
                    return NotFound("Product not found");
                case -1:
                    return BadRequest("Exception thrown in repository");
                default:
                    return Ok("Product deleted");
            }
        }
    }
}