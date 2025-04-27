using InventoryService.Context;
using InventoryService.Interfaces;
using InventoryService.Models;
using Microsoft.EntityFrameworkCore;

namespace InventoryService.Repositories;

public class CategoryRepository : ICategoryRepository {
	private readonly InventoryDbContext db;
	public CategoryRepository(InventoryDbContext context) {
		this.db = context;
	}
	public async Task<bool> AddCategoryAsync(Category category) {
		if(category == null) {
			throw new ArgumentNullException(nameof(category));
		}
		await db.Categories.AddAsync(category);
		var result = await db.SaveChangesAsync();
		if (result > 0) {
			return true;
		}
		else {
			return false;
		}
	}

	public async Task<bool> DeleteCategoryAsync(int id) {
		if(id <= 0) {
			throw new ArgumentOutOfRangeException(nameof(id));
		}
		var checkedItem = await db.Categories.FirstOrDefaultAsync(i => i.CategoryID == id);
		if (checkedItem == null) {
			return false;
		}
		db.Categories.Remove(checkedItem);
		var result = await db.SaveChangesAsync();
		if (result > 0) {
			return true;
		}
		else {
			return false;
		}
	}

	public async Task<ICollection<Category>> GetAllCategoriesAsync() {
		var lstCate = await db.Categories.ToListAsync();
		return lstCate;
	}

	public async Task<Category?> GetCategoryByIdAsync(int id) {
		var checkItem = await db.Categories.FirstOrDefaultAsync(i => i.CategoryID == id);
		if (checkItem == null) {
			return null;
		}
		return checkItem;
	}

}
