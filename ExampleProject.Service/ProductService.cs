using ExampleProject.Common;
using ExampleProject.Model;
using ExampleProject.Repository;
using ExampleProject.WebApi;

namespace ExampleProject.Service
{
    public class ProductService
    {
        public List<Product> GetAll(ProductFilter filter )
        {
            ProductRepository repository = new ProductRepository();
            return repository.GetAll(filter);
        }
        public Product Get(int id)
        {
            ProductRepository repository = new ProductRepository();
            return repository.Get(id);
        }
        public int Add(ProductCategoryDTO dto)
        {
            ProductRepository repository = new ProductRepository();
            Product product = dto.ToProduct();
            return repository.Add(product);
        }
        public int Delete(int id)
        {
            ProductRepository repository = new ProductRepository();
            return repository.Delete(id);
        }
        public int Update(int id, ProductCategoryDTO dto)
        {
            ProductRepository repository = new ProductRepository();
            Product product = dto.ToProduct();
            return repository.Update(id, product);
        }
    }
}
