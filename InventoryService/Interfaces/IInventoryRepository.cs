using InventoryService.Models;

namespace InventoryService.Interfaces;

public interface IInventoryRepository {
	Task<ICollection<InventoryItem>> GetAllItemsAsync();
	Task<ICollection<InventoryItem>> GetItemByListID(List<int> lstItemID);
	Task<ICollection<InventoryItem>> GetAllItemsByShopIDAsync(int shopID);
	Task<ICollection<InventoryItem>> GetAllItemsByCategoryIDAsync(int categoryID);
	Task<InventoryItem?> GetItemByIdAsync(int id);
	Task<InventoryItem?> GetItemByNameAsync(string name);
	Task<bool> AddItemAsync(InventoryItem item);
	Task<bool> UpdateItemAsync(InventoryItem item, int id);
	Task<bool> DeleteItemAsync(int id);
	Task<bool> AddListItemToShopAsync(List<int> lstItemID, int shopID);
}

