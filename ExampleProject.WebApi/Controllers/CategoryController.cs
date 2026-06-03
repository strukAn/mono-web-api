using Microsoft.AspNetCore.Mvc;

namespace ExampleProject.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CategoriesController : ControllerBase
    {
        [HttpGet("all")]
        public IEnumerable<Category> GetAll()
        {
            return Database.Categories.ToArray();
        }

        [HttpGet("{id}")]
        public Category Get(int id)
        {
            return Database.Categories.Find(category => category.Id == id);
        }

        [HttpDelete("delete")]
        public string Delete(int id)
        {
            Category category = Database.Categories.Find(category => category.Id == id);

            if (category == null)
            {
                return $"Category with id={id} not found";
            }

            Database.Categories.Remove(category);
            return "Category deleted";
        }

        [HttpPost("add")]
        public string Post(Category category)
        {
            Database.Categories.Add(category);

            return "Category added";
        }

        [HttpPut("update")]
        public string Put(int id, Category updated)
        {
            Category category = Database.Categories.Find(category => category.Id == id);

            if (category == null)
            {
                return $"Category with id={id} not found";
            }

            if (updated.Name != null && category.Name != updated.Name)
            {
                category.Name = updated.Name;
            }

            return "Category updated";
        }
    }
}
