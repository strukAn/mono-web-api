using ExampleProject.Common;
using ExampleProject.Model;

namespace ExampleProject.Repository.Common
{
    public interface IProductRepository
    {
        public Task<List<Product>> GetAllAsync(ProductFilter filter);
        public Task<Product> GetAsync(int id);
        public Task<int> DeleteAsync(int id);
        public Task<int> AddAsync(Product dto);
        public Task<int> UpdateAsync(int id, Product dto);
    }
}
