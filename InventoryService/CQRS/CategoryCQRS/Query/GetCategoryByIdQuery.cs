using InventoryService.Interfaces;
using InventoryService.Models;
using Mediator;

namespace InventoryService.CQRS.CategoryCQRS.Query;

public record GetCategoryByIdQuery(int id) : IRequest<Category>{
}
public class GetCategoryByIdQueryHandler : IRequestHandler<GetCategoryByIdQuery, Category> {
	private readonly ICategoryRepository _categoryRepository;
	public GetCategoryByIdQueryHandler(ICategoryRepository categoryRepository) {
		this._categoryRepository = categoryRepository;
	}
	public async ValueTask<Category> Handle(GetCategoryByIdQuery request, CancellationToken cancellationToken) {
		var result = await _categoryRepository.GetCategoryByIdAsync(request.id);
		return result;
	}
}
