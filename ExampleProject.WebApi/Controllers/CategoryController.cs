using ExampleProject.Common;
using ExampleProject.Model;
using ExampleProject.Service.Common;
using Microsoft.AspNetCore.Mvc;

namespace ExampleProject.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CategoriesController : ControllerBase
    {
        protected ICategoryService CategoryService { get; }
        public CategoriesController(ICategoryService categoryService)
        {
            CategoryService = categoryService;
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAllAsync([FromQuery] string name = "")
        {
            CategoryFilter filter = new CategoryFilter();

            filter.Name = name;

            List<Category> categories = await CategoryService.GetAllAsync(filter);

            if (categories == null)
            {
                return NotFound("No categories found");
            }

            return Ok(categories);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync(int id)
        {
            Category product = await CategoryService.GetAsync(id);

            if (product == null)
            {
                return NotFound("Category not found");
            }

            return Ok(product);
        }

        [HttpDelete("delete")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            int result = await CategoryService.DeleteAsync(id);

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
            int result = await CategoryService.AddAsync(category);

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
            int result = await CategoryService.UpdateAsync(id, updated);

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
