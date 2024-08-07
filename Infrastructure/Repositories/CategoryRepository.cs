using ThePokemonProject.Data;
using ThePokemonProject.Interfaces;
using ThePokemonProject.Models;

namespace ThePokemonProject.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly DataContext _dataContext;

        public CategoryRepository(DataContext dataContext)
        {
            this._dataContext = dataContext;
        }
        public bool CategoryExists(int id)
        {
            return _dataContext.Categories.Any(c => c.Id == id);
        }

        public bool CreateCategory(Category category)
        {
            _dataContext.Add(category);
            return Save();

        }

        public bool DeleteCategory(Category category)
        {
            _dataContext.Remove(category);
            return Save();
        }

        public ICollection<Category> GetCategories()
        {
            return _dataContext.Categories.ToList();
        }

        public Category GetCategory(int id)
        {
            return _dataContext.Categories.FirstOrDefault(e => e.Id == id);
        }

        public ICollection<Pokemon> GetPokemonByCategory(int categoryId)
        {
            ///////////////////////////////////////////////////////////////////////////////////////////////////
            return _dataContext.PokemonCategories
                .Where(e => e.CategoryId == categoryId).Select(c => c.Pokemon).ToList();
        }

        public bool Save()
        {
            var saved = _dataContext.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdateCategory(Category category)
        {
            _dataContext.Update(category);
            return Save();
        }
    }
}
