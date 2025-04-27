using InventoryService.Interfaces;
using InventoryService.Models;
using InventoryService.Models.DTO;
using MapsterMapper;
using Mediator;

namespace InventoryService.CQRS.Command;
public record CreateItemCommand(CreateItemRequest model) : IRequest<bool>{
}
public class CreateItemCommandHandler : IRequestHandler<CreateItemCommand, bool> {
	private readonly IInventoryRepository inventoryRepository;
	private readonly IMapper mapper;

	public CreateItemCommandHandler(IInventoryRepository inventoryRepository, IMapper mapper) {
		this.inventoryRepository = inventoryRepository;
		this.mapper = mapper;
	}

	public async ValueTask<bool> Handle(CreateItemCommand request, CancellationToken cancellationToken) {
		var item = mapper.Map<InventoryItem>(request.model);
		var result = await inventoryRepository.AddItemAsync(item);
		return result;
	}
}
