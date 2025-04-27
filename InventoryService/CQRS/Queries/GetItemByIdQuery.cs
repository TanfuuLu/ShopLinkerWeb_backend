using InventoryService.Interfaces;
using InventoryService.Models;
using Mediator;

namespace InventoryService.CQRS.Queries;

public record GetItemByIdQuery(int itemID) : IRequest<InventoryItem> {
}
public class GetItemByIdQueryHandler : IRequestHandler<GetItemByIdQuery, InventoryItem> {
	private readonly IInventoryRepository inventoryRepository;
	public GetItemByIdQueryHandler(IInventoryRepository inventoryRepository) {
		this.inventoryRepository = inventoryRepository;
	}
	public async ValueTask<InventoryItem> Handle(GetItemByIdQuery request, CancellationToken cancellationToken) {
		var result = await inventoryRepository.GetItemByIdAsync(request.itemID);
		return result;
	}
}
