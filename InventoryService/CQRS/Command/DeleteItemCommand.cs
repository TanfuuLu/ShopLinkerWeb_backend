using InventoryService.Interfaces;
using Mediator;

namespace InventoryService.CQRS.Command;

public record DeleteItemCommand(int itemID) : IRequest<bool> {
}
public class DeleteItemCommandHandler : IRequestHandler<DeleteItemCommand, bool> {
	private readonly IInventoryRepository inventoryRepository;
	public DeleteItemCommandHandler(IInventoryRepository inventoryRepository) {
		this.inventoryRepository = inventoryRepository;
	}
	public async ValueTask<bool> Handle(DeleteItemCommand request, CancellationToken cancellationToken) {
		var result = await inventoryRepository.DeleteItemAsync(request.itemID);
		return result;
	}
}	
