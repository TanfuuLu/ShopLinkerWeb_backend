using ShopService.Models;

namespace ShopService.Repositories.Interfaces;

public interface IShopRepository {
	Task<bool> CreateShop(Shop model);
	Task<ICollection<Shop>> GetAll();
	Task<Shop> GetById(int id);
	Task<bool> UpdateShop(Shop model);
	Task<bool> DeleteShop(int id);
	Task<bool> AddEmployee(int shopId, int userId);
	Task<bool> RemoveEmployee(int shopId, int userId);
}
