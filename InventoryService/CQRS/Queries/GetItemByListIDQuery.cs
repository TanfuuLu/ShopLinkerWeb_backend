using InventoryService.Interfaces;
using InventoryService.Models;
using Mediator;

namespace InventoryService.CQRS.Queries;

public record GetItemByListIDQuery(List<int> lstItemID) : IRequest<ICollection<InventoryItem>> {
}
public class GetItemByListIDQueryHandler : IRequestHandler<GetItemByListIDQuery, ICollection<InventoryItem> > {
	private readonly IInventoryRepository _inventoryRepository;
	public GetItemByListIDQueryHandler(IInventoryRepository inventoryRepository) {
		_inventoryRepository = inventoryRepository;
	}
	public async ValueTask<ICollection<InventoryItem>> Handle(GetItemByListIDQuery request, CancellationToken cancellationToken) {
		var result = await _inventoryRepository.GetItemByListID(request.lstItemID);
		return result;
	}
}
