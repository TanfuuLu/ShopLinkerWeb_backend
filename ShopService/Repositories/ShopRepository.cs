using MapsterMapper;
using Microsoft.EntityFrameworkCore;
using ShopService.Context;
using ShopService.Models;
using ShopService.Repositories.Interfaces;

namespace ShopService.Repositories;

public class ShopRepository : IShopRepository {
	private readonly ShopDbContext dbContext;
	private readonly IMapper mapper;

	public ShopRepository(ShopDbContext dbContext, IMapper mapper) {
		this.dbContext = dbContext;
		this.mapper = mapper;
	}

	public async Task<bool> AddEmployee(int shopId, int userId) {
		var itemList = await dbContext.Shops.FirstOrDefaultAsync(x => x.Id == shopId);
		if(itemList == null) {
			return false;
		} 
		itemList.Employee.Add(userId);
		
		var result = await dbContext.SaveChangesAsync();
		if(result > 0) {
			return true;
		} else {
			return false;
		}
	}

	public async Task<bool> CreateShop(Shop model) {

		await dbContext.Shops.AddAsync(model);
		var result = await dbContext.SaveChangesAsync();
		if(result > 0) {
			return true;
		} else {
			return false;
		}
	}

	public async Task<bool> DeleteShop(int id) {
		var item = await dbContext.Shops.FindAsync(id);
		if(item == null) {
			return false;
		}
		dbContext.Shops.Remove(item);
		var result = await dbContext.SaveChangesAsync();
		if(result > 0) {
			return true;
		} else {
			return false;
		}
	}

	public async Task<ICollection<Shop>> GetAll() {
		var list = await dbContext.Shops.ToListAsync();
		if(list.Count > 0) {
			return list;
		} else {
			return null;
		}
		
	}

	public async Task<Shop> GetById(int id) {
		var item = await dbContext.Shops.FirstOrDefaultAsync(x => x.Id == id);
		if(item == null) {
			return null;
		}	
		else {
			return item;
		}
	}

	public async Task<bool> RemoveEmployee(int shopId, int userId) {
		var list = await dbContext.Shops.FirstOrDefaultAsync(s => s.Id == shopId);
		if(list == null) {
			return false;
		}
		if(list.Employee.Contains(userId)) {
			list.Employee.Remove(userId);
		} else {
			return false;
		}
		await dbContext.SaveChangesAsync();
		return true;
	}

	public async Task<bool> UpdateShop(Shop model) {
		var item = await dbContext.Shops.FirstOrDefaultAsync(x => x.Id == model.Id);
		if(item == null) {
			return false;
		} else {
			item.Employee = model.Employee;
			item.ManagerEmail = model.ManagerEmail;
			item.ManagerName = model.ManagerName;
			item.ManagerPhoneNumber = model.ManagerPhoneNumber;
		}
		
		var result = await dbContext.SaveChangesAsync();
		if(result > 0) {
			return true;
		} else {
			return false;
		}
	}
}
