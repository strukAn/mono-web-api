using ExampleProject.Common;
using ExampleProject.Model;
using ExampleProject.Repository.Common;
using ExampleProject.Service.Common;

namespace ExampleProject.Service
{
    public class CategoryService : ICategoryService
    {
        protected ICategoryRepository CategoryRepository { get; }

        public CategoryService(ICategoryRepository categoryRepository)
        {
            CategoryRepository = categoryRepository;
        }

        public async Task<List<Category>> GetAllAsync(CategoryFilter filter)
        {
            return await CategoryRepository.GetAllAsync(filter);
        }
        public async Task<Category> GetAsync(int id)
        {
            return await CategoryRepository.GetAsync(id);
        }
        public async Task<int> AddAsync(Category category)
        {
            return await CategoryRepository.AddAsync(category);
        }
        public async Task<int> DeleteAsync(int id)
        {
            return await CategoryRepository.DeleteAsync(id);
        }
        public async Task<int> UpdateAsync(int id, Category category)
        {
            return await CategoryRepository.UpdateAsync(id, category);
        }
    }
}
