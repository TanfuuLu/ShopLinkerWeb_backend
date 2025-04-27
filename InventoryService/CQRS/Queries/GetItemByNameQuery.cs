using InventoryService.Interfaces;
using InventoryService.Models;
using Mediator;

namespace InventoryService.CQRS.Queries;

public record GetItemByNameQuery(string itemName) : IRequest<InventoryItem> {
}
public class GetItemByNameQueryHandler : IRequestHandler<GetItemByNameQuery, InventoryItem> {
	private readonly IInventoryRepository inventoryRepository;
	public GetItemByNameQueryHandler(IInventoryRepository inventoryRepository) {
		this.inventoryRepository = inventoryRepository;
	}
	public async ValueTask<InventoryItem> Handle(GetItemByNameQuery request, CancellationToken cancellationToken) {
		var result = await inventoryRepository.GetItemByNameAsync(request.itemName);
		return result;
	}
}
