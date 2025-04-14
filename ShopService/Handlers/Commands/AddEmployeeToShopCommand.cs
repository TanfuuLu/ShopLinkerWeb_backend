using MapsterMapper;
using Mediator;
using ShopService.Models.DTO;
using ShopService.Repositories.Interfaces;

namespace ShopService.Handlers.Commands;

public record AddEmployeeToShopCommand(int shopId, int idEmployee) : IRequest<GetShopResponse> {
}
public class AddEmployeeToShopCommandHandler : IRequestHandler<AddEmployeeToShopCommand, GetShopResponse> {
	private readonly IShopRepository _shopRepository;
	private readonly IMapper _mapper;
	public AddEmployeeToShopCommandHandler(IShopRepository shopRepository, IMapper mapper) {
		this._shopRepository = shopRepository;
		this._mapper = mapper;
	}
	public async ValueTask<GetShopResponse> Handle(AddEmployeeToShopCommand request, CancellationToken cancellationToken) {
		var result = await _shopRepository.AddEmployee(request.shopId, request.idEmployee);
		if(result == true) {
			var findItem = await _shopRepository.GetById(request.shopId);
			var mappedItem = _mapper.Map<GetShopResponse>(findItem);
			return mappedItem;
		} else {
			throw new Exception("Failed to add employee to shop");
		}
	}
}
