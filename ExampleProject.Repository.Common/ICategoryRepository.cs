using ExampleProject.Common;
using ExampleProject.Model;

namespace ExampleProject.Repository.Common
{
    public interface ICategoryRepository
    {
        public Task<List<Category>> GetAllAsync(CategoryFilter filter);
        public Task<Category> GetAsync(int id);
        public Task<int> DeleteAsync(int id);
        public Task<int> AddAsync(Category category);
        public Task<int> UpdateAsync(int id, Category category);
    }
}
