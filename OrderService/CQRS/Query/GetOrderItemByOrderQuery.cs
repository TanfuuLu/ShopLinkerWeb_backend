using Mediator;
using OrderService.Interfaces;
using OrderService.Models;

namespace OrderService.CQRS.Query;

public record GetOrderItemByOrderQuery(int orderID) : IRequest<ICollection<OrderItem>> {
}
public class GetOrderItemByOrderQueryHandler : IRequestHandler<GetOrderItemByOrderQuery, ICollection<OrderItem>> {
	private readonly IOrderItemRepository orderItemRepository;
	public GetOrderItemByOrderQueryHandler(IOrderItemRepository orderItemRepository) {
		this.orderItemRepository = orderItemRepository;
	}
	public async ValueTask<ICollection<OrderItem>> Handle(GetOrderItemByOrderQuery request, CancellationToken cancellationToken) {
		var result = await orderItemRepository.GetOrderItemsOfOrderAsync(request.orderID);
		return result;
	}
}

