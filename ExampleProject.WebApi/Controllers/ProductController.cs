using AutoMapper;
using ExampleProject.Common;
using ExampleProject.Model;
using ExampleProjest.Service.Common;
using Microsoft.AspNetCore.Mvc;

namespace ExampleProject.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductsController : ControllerBase
    {
        protected IProductService ProductService { get; }
        protected IMapper Mapper { get; }

        public ProductsController(IProductService productService, IMapper mapper)
        {
            this.ProductService = productService;
            this.Mapper = mapper;
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAllAsync([FromQuery] int stock = -1, [FromQuery] string name = "", [FromQuery] string description = "")
        {
            ProductFilter filter = new ProductFilter();

            filter.Name = name;
            filter.Stock = stock;
            filter.Description = description;

            List<Product> products = await ProductService.GetAllAsync(filter);

            if(products == null)
            {
                return NotFound("No products found");
            }

            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            Product product = await ProductService.GetAsync(id);

            if(product == null)
            {
                return NotFound("Product not found");
            }

            return Ok(product);
        }

        [HttpDelete("delete")]
        public async Task<IActionResult> Delete(int id)
        {
            int result = await ProductService.DeleteAsync(id);

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
        public async Task<IActionResult> Post(ProductCategoryDTO productDto)
        {
            Product product = Mapper.Map<Product>(productDto);
            int result = await ProductService.AddAsync(product);

            switch (result)
            {
                case 0:
                    return BadRequest("Product not added");
                case -1:
                    return BadRequest("Exception thrown in repository");
                default:
                    return Ok("Product added");
            }
        }

        [HttpPut("update")]
        public async Task<IActionResult> Put(int id, ProductCategoryDTO updated)
        {
            Product product = Mapper.Map<Product>(updated);
            int result = await ProductService.UpdateAsync(id, product);

            switch (result)
            {
                case 0:
                    return NotFound("Product not found");
                case -1:
                    return BadRequest("Exception thrown in repository");
                default:
                    return Ok("Product updated");
            }
        }
    }
}