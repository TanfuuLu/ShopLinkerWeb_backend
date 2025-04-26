using InventoryService.Models;

namespace InventoryService.Interfaces;

public interface IInventoryRepository {
	Task<ICollection<InventoryItem>> GetAllItemsAsync();
	Task<ICollection<InventoryItem>> GetAllItemsByShopIDAsync(int shopID);
	Task<InventoryItem?> GetItemByIdAsync(int id);
	Task<InventoryItem?> GetItemByNameAsync(string name);
	Task<bool> AddItemAsync(InventoryItem item);
	Task<bool> UpdateItemAsync(InventoryItem item);
	Task<bool> DeleteItemAsync(int id);
	Task<bool> AddListItemToShopAsync(List<int> lstItemID, int shopID);
}

