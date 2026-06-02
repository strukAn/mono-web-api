using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace ExampleProject.WebApi
{
    public class ProductCategoryDTO
    {
        public int ProductId {  get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public int Stock { get; set; }
        public int CategoryId { get; set; }
        public string? CategoryName { get; set; }
    }
}
