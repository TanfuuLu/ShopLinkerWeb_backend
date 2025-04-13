using MapsterMapper;
using Mediator;
using ShopService.Models;
using ShopService.Models.DTO;
using ShopService.Repositories.Interfaces;

namespace ShopService.Handlers.Commands;

public record CreateShopCommand(CreateShopRequest model) : IRequest<Shop>{
}
public class CreateShopCommandHandler : IRequestHandler<CreateShopCommand, Shop> {
	private readonly IShopRepository _shopRepository;
	private readonly IMapper _mapper;

	public CreateShopCommandHandler(IShopRepository shopRepository, IMapper mapper) {
		this._shopRepository = shopRepository;
		this._mapper = mapper;
	}
	public async ValueTask<Shop> Handle(CreateShopCommand request, CancellationToken cancellationToken) {
		var model = _mapper.Map<Shop>(request.model);
		model.Employee = new List<int>();
		model.ManagerEmail = "";
		model.ManagerName = "";
		model.ManagerPhoneNumber = "";
		var result = await _shopRepository.CreateShop(model);
		if(result == true) {
			var item = await _shopRepository.GetById(model.Id);
			return item;
		} else {
			throw new Exception("Failed to create shop");
		}
		
	}
}
