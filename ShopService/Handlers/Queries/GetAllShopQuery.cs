using MapsterMapper;
using Mediator;
using ShopService.Models;
using ShopService.Models.DTO;
using ShopService.Repositories.Interfaces;

namespace ShopService.Handlers.Queries;

public record GetAllShopQuery : IRequest<ICollection<GetShopResponse>> {
}
public class GetAllShopQueryHandler : IRequestHandler<GetAllShopQuery, ICollection<GetShopResponse>> {
	private readonly IShopRepository _shopRepository;
	private readonly IMapper mapper;
	public GetAllShopQueryHandler(IShopRepository shopRepository, IMapper mapper) {
		this._shopRepository = shopRepository;
		this.mapper = mapper;
	}
	public async ValueTask<ICollection<GetShopResponse>> Handle(GetAllShopQuery request, CancellationToken cancellationToken) {
		var result = await _shopRepository.GetAll();
		var resultMapped = mapper.Map<List<GetShopResponse>>(result);
		return resultMapped;
	}
}