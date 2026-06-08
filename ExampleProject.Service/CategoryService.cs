using ExampleProject.Common;
using ExampleProject.Model;
using ExampleProject.Repository;

namespace ExampleProject.Service
{
    public class CategoryService
    {
        public async Task<List<Category>> GetAllAsync(CategoryFilter filter)
        {
            CategoryRepository repository = new CategoryRepository();
            return await repository.GetAllAsync(filter);
        }
        public async Task<Category> GetAsync(int id)
        {
            CategoryRepository repository = new CategoryRepository();
            return await repository.GetAsync(id);
        }
        public async Task<int> AddAsync(Category category)
        {
            CategoryRepository repository = new CategoryRepository();
            return await repository.AddAsync(category);
        }
        public async Task<int> DeleteAsync(int id)
        {
            CategoryRepository repository = new CategoryRepository();
            return await repository.DeleteAsync(id);
        }
        public async Task<int> UpdateAsync(int id, Category category)
        {
            CategoryRepository repository = new CategoryRepository();
            return await repository.UpdateAsync(id, category);
        }
    }
}
