using InventoryService.Interfaces;
using InventoryService.Models;
using InventoryService.Models.DTO;
using MapsterMapper;
using Mediator;

namespace InventoryService.CQRS.CategoryCQRS.Command;

public record CreateCategoryCommand(CreateCategoryRequest model) : IRequest<bool> {
}
public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand, bool> {
	private readonly ICategoryRepository _categoryRepository;
	private readonly IMapper mapper;

	public CreateCategoryCommandHandler(ICategoryRepository categoryRepository, IMapper mapper) {
		this._categoryRepository = categoryRepository;
		this.mapper = mapper;
	}

	public async ValueTask<bool> Handle(CreateCategoryCommand request, CancellationToken cancellationToken) {
		var mappedItem = mapper.Map<Category>(request.model);
		var result = await _categoryRepository.AddCategoryAsync(mappedItem);
		return result;
	}
}	
