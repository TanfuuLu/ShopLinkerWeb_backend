using MapsterMapper;
using Mediator;
using OrderService.Interfaces;
using OrderService.Models;
using OrderService.Models.DTO;

namespace OrderService.CQRS.Command;

public record CreateListOrderItemCommand(List<CreateOrderItemRequest> listItem): IRequest<bool> {
}
public class CreateListOrderItemCommandHandler : IRequestHandler<CreateListOrderItemCommand, bool> {
	private readonly IOrderItemRepository orderItemRepository;
	private readonly IMapper mapper;

	public CreateListOrderItemCommandHandler(IOrderItemRepository orderItemRepository, IMapper mapper) {
		this.orderItemRepository = orderItemRepository;
		this.mapper = mapper;
	}

	public async ValueTask<bool> Handle(CreateListOrderItemCommand request, CancellationToken cancellationToken) {
		var mappedListItem = mapper.Map<ICollection<OrderItem>>(request.listItem);
		var result = await orderItemRepository.CreateListOrderItemsAsync(mappedListItem);
		return result;
	}
}
