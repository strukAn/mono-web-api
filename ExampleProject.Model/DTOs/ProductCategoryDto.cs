using ExampleProject.Model;

namespace ExampleProject.WebApi
{
    public class ProductCategoryDTO
    {
        public int ProductId { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public int Stock { get; set; }
        public int CategoryId { get; set; }
        public string? CategoryName { get; set; }

        public Product ToProduct()
        {
            Category category = new Category(this.CategoryId, this.CategoryName);

            Product product = new Product(this.ProductId, this.Name, this.Description, this.Stock, category);

            return product;
        }
    }
}
