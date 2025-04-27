using InventoryService.Interfaces;
using Mediator;

namespace InventoryService.CQRS.CategoryCQRS.Command;

public record DeleteCategoryCommand(int id): IRequest<bool> {
}
public class DeleteCategoryCommandHandler : IRequestHandler<DeleteCategoryCommand, bool> {
	private readonly ICategoryRepository _categoryRepository;
	public DeleteCategoryCommandHandler(ICategoryRepository categoryRepository) {
		this._categoryRepository = categoryRepository;
	}
	public async ValueTask<bool> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken) {
		var result = await _categoryRepository.DeleteCategoryAsync(request.id);
		return result;
	}
}
