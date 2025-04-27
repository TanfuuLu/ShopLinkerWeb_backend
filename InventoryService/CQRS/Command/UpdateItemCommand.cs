using InventoryService.Interfaces;
using InventoryService.Models;
using InventoryService.Models.DTO;
using MapsterMapper;
using Mediator;

namespace InventoryService.CQRS.Command;

public record UpdateItemCommand(UpdateItemRequest model, int id) : IRequest<bool> {
}
public class UpdateItemCommandHandler : IRequestHandler<UpdateItemCommand, bool> {
	private readonly IInventoryRepository inventoryRepository;
	private readonly IMapper mapper;

	public UpdateItemCommandHandler(IInventoryRepository inventoryRepository, IMapper mapper) {
		this.inventoryRepository = inventoryRepository;
		this.mapper = mapper;
	}

	public async ValueTask<bool> Handle(UpdateItemCommand request, CancellationToken cancellationToken) {
		var mappedItem = mapper.Map<InventoryItem>(request.model);
		var result = await  inventoryRepository.UpdateItemAsync(mappedItem, request.id);
		return result;
	}
}
