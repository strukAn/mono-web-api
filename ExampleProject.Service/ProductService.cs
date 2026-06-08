using ExampleProject.Common;
using ExampleProject.Model;
using ExampleProject.Repository;
using ExampleProject.WebApi;

namespace ExampleProject.Service
{
    public class ProductService
    {
        public async Task<List<Product>> GetAllAsync(ProductFilter filter )
        {
            ProductRepository repository = new ProductRepository();
            return await repository.GetAllAsync(filter);
        }
        public async Task<Product> GetAsync(int id)
        {
            ProductRepository repository = new ProductRepository();
            return await repository.GetAsync(id);
        }
        public async Task<int> AddAsync(ProductCategoryDTO dto)
        {
            ProductRepository repository = new ProductRepository();
            Product product = dto.ToProduct();
            return await repository.AddAsync(product);
        }
         public async Task<int> DeleteAsync(int id)
        {
            ProductRepository repository = new ProductRepository();
            return await repository.DeleteAsync(id);
        }
         public async Task<int> UpdateAsync(int id, ProductCategoryDTO dto)
        {
            ProductRepository repository = new ProductRepository();
            Product product = dto.ToProduct();
            return await repository.UpdateAsync(id, product);
        }
    }
}
