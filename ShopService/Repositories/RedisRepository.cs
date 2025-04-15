using Newtonsoft.Json;
using ShopService.Models;
using ShopService.Repositories.Interfaces;
using StackExchange.Redis;

namespace ShopService.Repositories;

public class RedisRepository : IRedisRepository {
	private readonly IConnectionMultiplexer redis;
	private readonly IDatabase db;

	public RedisRepository(IConnectionMultiplexer redis) {
		this.redis = redis;
		this.db = redis.GetDatabase();
	}

	public async Task<Shop> GetItemAsync(string keyItem) {
		var value = await db.StringGetAsync(keyItem);
		if(!value.HasValue) {
			return null;
		} else {
			var rawJson = JsonConvert.DeserializeObject<string>(value);

			// Deserialize lần 2 để lấy đối tượng Shop
			var item = JsonConvert.DeserializeObject<Shop>(rawJson);
			return item;
		}
	}

	public async Task<List<Shop>> GetListItem(string keyList, List<Shop> list) {
		var listCache = await db.StringGetAsync(keyList);
		if(listCache.HasValue) {
			return JsonConvert.DeserializeObject<List<Shop>>(listCache);
		} else {
			return null;
		}
	}

	public async Task RemoveAsync(string keyItem) {
		await db.KeyDeleteAsync(keyItem);
	}

	public async Task SetItem(string keyItem, string valueItem) {
		var itemJson = JsonConvert.SerializeObject(valueItem);
		await db.StringSetAsync(keyItem, itemJson);

	}

	public async Task SetListItem(string keyList, List<Shop> list) {
		var itemListJson = JsonConvert.SerializeObject(list);
		await db.StringSetAsync(keyList, itemListJson);
	}
}
