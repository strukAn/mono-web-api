using Microsoft.AspNetCore.Mvc;

namespace ExampleProject.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CategoriesController : ControllerBase
    {
        [HttpGet("all")]
        public IActionResult GetAll()
        {
            if (Database.Categories.Count == 0)
            {
                return NotFound("There are no categories");
            }

            return Ok(Database.Categories);
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            return Ok(Database.Categories.Find(category => category.Id == id));
        }

        [HttpDelete("delete")]
        public IActionResult Delete(int id)
        {
            Category category = Database.Categories.Find(category => category.Id == id);

            if (category == null)
            {
                return NotFound($"Category with id={id} not found");
            }

            Database.Categories.Remove(category);
            return Ok("Category deleted");
        }

        [HttpPost("add")]
        public IActionResult Post(Category category)
        {
            Database.Categories.Add(category);

            return Created("","Category added");
        }

        [HttpPut("update")]
        public IActionResult Put(int id, Category updated)
        {
            Category category = Database.Categories.Find(category => category.Id == id);

            if (category == null)
            {
                return NotFound($"Category with id={id} not found");
            }

            if (updated.Name != null && category.Name != updated.Name)
            {
                category.Name = updated.Name;
            }

            return Ok("Category updated");
        }
    }
}
