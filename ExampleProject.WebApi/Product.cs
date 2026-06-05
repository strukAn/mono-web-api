using ExampleProject.WebApi.Controllers;

namespace ExampleProject.WebApi
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Stock { get; set; }

        public Category? Category { get; set; }

        public Product(int id, string name, string description, int stock, Category category)
        {
            this.Id = id;
            this.Name = name;
            this.Description = description;
            this.Stock = stock;
            this.Category = category;
        }

        public Product(string name, string description, int stock, Category category)
        {
            this.Id = Database.Products.Last().Id + 1;
            this.Name = name;
            this.Description = description;
            this.Stock = stock;
            this.Category = category;
        }
        public Product(string name, string description, Category category)
        {
            this.Id = Database.Products.Last().Id + 1;
            this.Name = name;
            this.Description = description;
            this.Category = category;
        }
        public Product(string name, Category category)
        {
            this.Id = Database.Products.Last().Id + 1;
            this.Name = name;
            this.Category = category;
        }

        public Product() { }
    }
}
