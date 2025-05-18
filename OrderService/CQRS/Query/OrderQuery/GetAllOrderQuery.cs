using Mediator;
using OrderService.Interfaces;
using OrderService.Models;

namespace OrderService.CQRS.Query.OrderQuery;

public record GetAllOrderQuery : IRequest<ICollection<Order>> {
}
public class GetAllOrderQueryHandler : IRequestHandler<GetAllOrderQuery, ICollection<Order>> {
	private readonly IOrderRepository orderRepository;
	public GetAllOrderQueryHandler(IOrderRepository orderRepository) {
		this.orderRepository = orderRepository;
	}
	public async ValueTask<ICollection<Order>> Handle(GetAllOrderQuery request, CancellationToken cancellationToken) {
		var result = await orderRepository.GetAllOrdersAsync();
		return result;
	}
}

