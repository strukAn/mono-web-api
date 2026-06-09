using ExampleProject.Common;
using ExampleProject.Model;
using ExampleProject.Repository.Common;
using ExampleProjest.Service.Common;

namespace ExampleProject.Service
{
    public class ProductService : IProductService
    {
        protected IProductRepository ProductRepository { get; }
        public ProductService(IProductRepository repository) {
            this.ProductRepository = repository;
        }
        public async Task<List<Product>> GetAllAsync(ProductFilter filter )
        {
            return await ProductRepository.GetAllAsync(filter);
        }
        public async Task<Product> GetAsync(int id)
        {
            return await ProductRepository.GetAsync(id);
        }
        public async Task<int> AddAsync(Product product)
        {
            return await ProductRepository.AddAsync(product);
        }
         public async Task<int> DeleteAsync(int id)
        {
            return await ProductRepository.DeleteAsync(id);
        }
         public async Task<int> UpdateAsync(int id, Product product)
        {
            return await ProductRepository.UpdateAsync(id, product);
        }
    }
}
