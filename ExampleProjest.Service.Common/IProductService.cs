using ExampleProject.Common;
using ExampleProject.Model;
using ExampleProject.WebApi;

namespace ExampleProjest.Service.Common
{
    public interface IProductService
    {
        public Task<List<Product>> GetAllAsync(ProductFilter filter);
        public Task<Product> GetAsync(int id);
        public Task<int> AddAsync(Product product);
        public Task<int> DeleteAsync(int id);
        public Task<int> UpdateAsync(int id, Product product);
    }
}
