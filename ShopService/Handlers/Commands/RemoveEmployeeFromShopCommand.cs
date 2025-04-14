using MapsterMapper;
using Mediator;
using ShopService.Models.DTO;
using ShopService.Repositories.Interfaces;

namespace ShopService.Handlers.Commands;

public record RemoveEmployeeFromShopCommand(int shopId, int empId) : IRequest<GetShopResponse> {
}
public class RemoveEmployeeFromShopCommandHandler : IRequestHandler<RemoveEmployeeFromShopCommand, GetShopResponse> {
	private readonly IShopRepository _shopRepository;
	private readonly IMapper _mapper;
	public RemoveEmployeeFromShopCommandHandler(IShopRepository shopRepository, IMapper mapper) {
		this._shopRepository = shopRepository;
		this._mapper = mapper;
	}
	public async ValueTask<GetShopResponse> Handle(RemoveEmployeeFromShopCommand request, CancellationToken cancellationToken) {
		var result = await _shopRepository.RemoveEmployee(request.shopId, request.empId);
		if(result == true) {
			var findItem = await _shopRepository.GetById(request.shopId);
			var mappedItem = _mapper.Map<GetShopResponse>(findItem);
			return mappedItem;
		} else {
			throw new Exception("Failed to remove employee from shop");
		}
	}
}
