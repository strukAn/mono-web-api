using ExampleProject.Common;
using ExampleProject.Model;

namespace ExampleProject.Repository.Common
{
    public interface IProductRepository
    {
        public List<Product> GetAll(ProductFilter filter);
        public Product Get(int id);
        public int Delete(int id);
        public int Add(Product dto);
        public int Update(int id, Product dto);
    }
}
