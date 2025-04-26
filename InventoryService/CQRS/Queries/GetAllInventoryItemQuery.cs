using InventoryService.Interfaces;
using InventoryService.Models;
using Mediator;

namespace InventoryService.CQRS.Queries;

public record GetAllInventoryItemQuery : IRequest<ICollection<InventoryItem>>{
}
public class GetAllInventoryItemQueryHandler : IRequestHandler<GetAllInventoryItemQuery, ICollection<InventoryItem>> {
	private readonly IInventoryRepository inventoryRepository;

	public GetAllInventoryItemQueryHandler(IInventoryRepository inventoryRepository) {
		this.inventoryRepository = inventoryRepository;
	}

	public async ValueTask<ICollection<InventoryItem>> Handle(GetAllInventoryItemQuery request, CancellationToken cancellationToken) {
		var result = await inventoryRepository.GetAllItemsAsync();
		return result;
	}
}

