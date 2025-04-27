using InventoryService.Interfaces;
using InventoryService.Models;
using Mediator;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InventoryService.CQRS.Queries;
public record GetItemByCategoryIdQuery(int categoryID) : IRequest<ICollection<InventoryItem>>  {
}
public class GetItemByCategoryIdQueryHandler : IRequestHandler<GetItemByCategoryIdQuery, ICollection<InventoryItem>> {
	private readonly IInventoryRepository _inventoryRepository;
	public GetItemByCategoryIdQueryHandler(IInventoryRepository inventoryRepository) {
		this._inventoryRepository = inventoryRepository;
	}
	public async ValueTask<ICollection<InventoryItem>> Handle(GetItemByCategoryIdQuery request, CancellationToken cancellationToken) {
		var result = await _inventoryRepository.GetAllItemsByCategoryIDAsync(request.categoryID);
		return result;
	}
}
