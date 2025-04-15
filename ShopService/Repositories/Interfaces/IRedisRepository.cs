using ShopService.Models;

namespace ShopService.Repositories.Interfaces;

public interface IRedisRepository{
	Task<Shop> GetItemAsync(string keyItem);
	Task SetItem(string keyItem, string valueItem);	
	Task RemoveAsync(string keyItem);
	Task<List<Shop>> GetListItem(string keyList, List<Shop> list);
	Task SetListItem(string keyList, List<Shop> list);

}
