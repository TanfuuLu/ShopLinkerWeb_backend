using MapsterMapper;
using Mediator;
using Newtonsoft.Json;
using ShopService.Models;
using ShopService.Models.DTO;
using ShopService.Repositories.Interfaces;

namespace ShopService.Handlers.Queries;

public record GetShopByIdQuery(int id) : IRequest<GetShopResponse> {
}
public class GetShopByIdQueryHandler : IRequestHandler<GetShopByIdQuery, GetShopResponse> {
	private readonly IShopRepository _shopRepository;
	private readonly IMapper _mapper;
	private readonly IRedisRepository _redisRepository;

	public GetShopByIdQueryHandler(IShopRepository shopRepository, IMapper mapper, IRedisRepository redisRepository) {
		this._shopRepository = shopRepository;
		this._mapper = mapper;
		this._redisRepository = redisRepository;
	}

	public async ValueTask<GetShopResponse> Handle(GetShopByIdQuery request, CancellationToken cancellationToken) {
		var cacheKey = $"shop:{request.id}";
		var cachedShop = await _redisRepository.GetItemAsync(cacheKey);
		if(cachedShop == null) {
			var result = await _shopRepository.GetById(request.id);
			if(result != null) {
				var mappedItem = _mapper.Map<GetShopResponse>(result);
				var jsonItem = JsonConvert.SerializeObject(result);
				await _redisRepository.SetItem(cacheKey, jsonItem);
				return mappedItem;
			} else {
				return null;
			}
		} else {
			var mapItem = _mapper.Map<GetShopResponse>(cachedShop);
			return mapItem;
		}
	}
}
