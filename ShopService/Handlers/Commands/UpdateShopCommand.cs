using MapsterMapper;
using Mediator;
using ShopService.Models;
using ShopService.Models.DTO;
using ShopService.Repositories.Interfaces;

namespace ShopService.Handlers.Commands;

public record UpdateShopCommand(UpdateShopRequest input) : IRequest<bool> {

}
public class UpdateShopCommandHandler : IRequestHandler<UpdateShopCommand, bool> {
	private readonly IShopRepository _shopRepository;
	private readonly IMapper _mapper;
	public UpdateShopCommandHandler(IShopRepository shopRepository, IMapper mapper) {
		this._shopRepository = shopRepository;
		this._mapper = mapper;
	}
	public async ValueTask<bool> Handle(UpdateShopCommand request, CancellationToken cancellationToken) {
		var model = _mapper.Map<Shop>(request.input);
		model.Id = request.input.Id;
		var result = await _shopRepository.UpdateShop(model);
		if(result == true) {
			return result;
		} else {
			throw new Exception("Failed to update shop");
		}
	}
}
