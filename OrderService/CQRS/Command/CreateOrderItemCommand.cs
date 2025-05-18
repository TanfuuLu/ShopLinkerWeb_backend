using MapsterMapper;
using Mediator;
using OrderService.Interfaces;
using OrderService.Models;
using OrderService.Models.DTO;

namespace OrderService.CQRS.Command;

public record CreateOrderItemCommand(CreateOrderItemRequest model) : IRequest<bool> {
}
public class CreateOrderItemCommandHandler : IRequestHandler<CreateOrderItemCommand, bool> {
	private readonly IOrderItemRepository orderItemRepository;
	private readonly IMapper mapper;

	public CreateOrderItemCommandHandler(IOrderItemRepository orderItemRepository, IMapper mapper) {
		this.orderItemRepository = orderItemRepository;
		this.mapper = mapper;
	}

	public async ValueTask<bool> Handle(CreateOrderItemCommand request, CancellationToken cancellationToken) {
		var mappedItem = mapper.Map<OrderItem>(request.model);
		var result = await orderItemRepository.CreateOrderItemAsync(mappedItem);
		return result;
	}
}
