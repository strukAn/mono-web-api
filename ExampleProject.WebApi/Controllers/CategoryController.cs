using ExampleProject.Common;
using ExampleProject.Model;
using ExampleProject.Service;
using Microsoft.AspNetCore.Mvc;

namespace ExampleProject.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CategoriesController : ControllerBase
    {
        [HttpGet("all")]
        public async Task<IActionResult> GetAllAsync([FromQuery] string name = "")
        {
            CategoryService service = new CategoryService();
            CategoryFilter filter = new CategoryFilter();

            filter.Name = name;

            List<Category> categories = await service.GetAllAsync(filter);

            if (categories == null)
            {
                return NotFound("No categories found");
            }

            return Ok(categories);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync(int id)
        {
            CategoryService service = new CategoryService();
            Category product = await service.GetAsync(id);

            if (product == null)
            {
                return NotFound("Category not found");
            }

            return Ok(product);
        }

        [HttpDelete("delete")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            CategoryService service = new CategoryService();
            int result = await service.DeleteAsync(id);

            switch (result)
            {
                case 0:
                    return NotFound("Category not found");
                case -1:
                    return BadRequest("Exception thrown in repository");
                default:
                    return Ok("Category deleted");
            }
        }

        [HttpPost("add")]
        public async Task<IActionResult> PostAsync(Category category)
        {
            CategoryService service = new CategoryService();
            int result = await service.AddAsync(category);

            switch (result)
            {
                case 0:
                    return BadRequest("Product not added");
                case -1:
                    return BadRequest("Exception thrown in repository");
                default:
                    return Ok("Category added");
            }
        }

        [HttpPut("update")]
        public async Task<IActionResult> PutAsync(int id, Category updated)
        {
            CategoryService service = new CategoryService();
            int result = await service.UpdateAsync(id, updated);

            switch (result)
            {
                case 0:
                    return NotFound("Category not found");
                case -1:
                    return BadRequest("Exception thrown in repository");
                default:
                    return Ok("Category updated");
            }
        }
    }
}
