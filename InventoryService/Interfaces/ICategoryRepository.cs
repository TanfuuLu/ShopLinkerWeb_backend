using InventoryService.Models;

namespace InventoryService.Interfaces;

public interface ICategoryRepository {
	Task<ICollection<Category>> GetAllCategoriesAsync();
	Task<Category?> GetCategoryByIdAsync(int id);
	Task<bool> AddCategoryAsync(Category category);
	Task<bool> DeleteCategoryAsync(int id);
}
