using System.Reflection.Metadata.Ecma335;
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
            if (Database.Products.Count == 0)
            {
                return NotFound();
            }

            var filtered = Database.Products.AsEnumerable();

            if(name != "")
            {
                filtered = filtered.Where(el => el.Name == name);
            }
            if (description != "")
            {
                filtered = filtered.Where(el => el.Description.IndexOf(description) != -1);
            }
            if (stock != -1)
            {
                filtered = filtered.Where(el => el.Stock == stock);
            }

            return Ok(filtered);
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            return Ok(Database.Products.Find(product => product.Id == id));
        }

        [HttpDelete("delete")]
        public IActionResult Delete(int id)
        {
            Product product = Database.Products.Find(product => product.Id == id);

            if (product == null)
            {
                return NotFound($"Product with id={id} not found");
            }

            Database.Products.Remove(product);
            return Ok("Product deleted");
        }

        [HttpPost("add")]
        public IActionResult Post(ProductCategoryDTO productDto)
        {
            if(productDto.CategoryId == 0)
            {
                return UnprocessableEntity($"CategoryId must be sent");
            }

            Category category = Database.Categories.Find(cat => cat.Id == productDto.CategoryId);
            if(category == null)
            {
                return UnprocessableEntity($"Category with id={productDto.CategoryId} doesn't exist"); 
            }

            Product product = new Product(productDto.Name, productDto.Description, category);

            product.Category = Database.Categories.Find(category => category.Id == productDto.CategoryId);

            if (product.Category == null)
            {
                return UnprocessableEntity($"Category with id={productDto.CategoryId} doesn't exist");
            }

            Database.Products.Add(product);
            return Created("", "Product created");
        }

        [HttpPut("update")]
        public IActionResult Put(int id, ProductCategoryDTO updated)
        {
            Product product = Database.Products.Find(product => product.Id == id);

            if (product == null)
            {
                return NotFound($"Product with id={id} not found");
            }

            if (updated.Name != null && product.Name != updated.Name)
            {
                product.Name = updated.Name;
            }

            if (updated.Description != null && product.Description != updated.Description)
            {
                product.Description = updated.Description;
            }

            if (updated.Stock != 0 && product.Stock != updated.Stock)
            {
                product.Stock = updated.Stock;
            }

            if (updated.CategoryId != 0)
            {
                Category category = Database.Categories.Find(cat => cat.Id == updated.CategoryId);
                if (category != null)
                {
                    product.Category = category;
                }
            }

            return Created("", "Product updated");
        }
    }
}