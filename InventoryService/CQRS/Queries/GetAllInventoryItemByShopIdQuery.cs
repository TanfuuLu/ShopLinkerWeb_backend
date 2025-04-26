using InventoryService.Interfaces;
using InventoryService.Models;
using Mediator;

namespace InventoryService.CQRS.Queries;

public record GetAllInventoryItemByShopIdQuery(int shopID) : IRequest<ICollection<InventoryItem>>{
}
public class GetAllInventoryItemByShopIdQueryHandler : IRequestHandler<GetAllInventoryItemByShopIdQuery, ICollection<InventoryItem>> {
	private readonly IInventoryRepository inventoryRepository;

	public GetAllInventoryItemByShopIdQueryHandler(IInventoryRepository inventoryRepository) {
		this.inventoryRepository = inventoryRepository;
	}

	public async ValueTask<ICollection<InventoryItem>> Handle(GetAllInventoryItemByShopIdQuery request, CancellationToken cancellationToken) {
		var result = await inventoryRepository.GetAllItemsByShopIDAsync(request.shopID);
		return result;
	}
}
