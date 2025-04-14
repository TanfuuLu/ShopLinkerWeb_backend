using MapsterMapper;
using Mediator;
using ShopService.Models;
using ShopService.Models.DTO;
using ShopService.Repositories.Interfaces;

namespace ShopService.Handlers.Queries;

public record GetShopByIdQuery(int id) : IRequest<GetShopResponse> {
}
public class GetShopByIdQueryHandler : IRequestHandler<GetShopByIdQuery, GetShopResponse> {
	private readonly IShopRepository _shopRepository;
	private readonly IMapper _mapper;

	public GetShopByIdQueryHandler(IShopRepository shopRepository, IMapper mapper) {
		this._shopRepository = shopRepository;
		this._mapper = mapper;
	}

	public async ValueTask<GetShopResponse> Handle(GetShopByIdQuery request, CancellationToken cancellationToken) {
		var result = await _shopRepository.GetById(request.id);
		var mappedItem = _mapper.Map<GetShopResponse>(result);
		if(result == null) {
			throw new Exception("Shop not found");
		} else {
			return mappedItem;
		}

	}
}
