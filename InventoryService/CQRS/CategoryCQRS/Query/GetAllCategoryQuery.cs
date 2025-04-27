using InventoryService.Interfaces;
using InventoryService.Models;
using Mediator;

namespace InventoryService.CQRS.CategoryCQRS.Query;

public record GetAllCategoryQuery : IRequest<ICollection<Category>> {
}
public class GetAllCategoryQueryHandler : IRequestHandler<GetAllCategoryQuery, ICollection<Category>> {
	private readonly ICategoryRepository _categoryRepository;
	public GetAllCategoryQueryHandler(ICategoryRepository categoryRepository) {
		this._categoryRepository = categoryRepository;
	}
	public async ValueTask<ICollection<Category>> Handle(GetAllCategoryQuery request, CancellationToken cancellationToken) {
		var result = await _categoryRepository.GetAllCategoriesAsync();
		return result;
	}
}
