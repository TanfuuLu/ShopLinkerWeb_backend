using OrderService.Models;

namespace OrderService.Interfaces;

public interface IOrderItemRepository {
	Task<ICollection<OrderItem>> GetOrderItemsOfOrderAsync(int orderId);
	Task<bool> CreateOrderItemAsync(OrderItem orderItem);
	Task<bool> CreateListOrderItemsAsync(ICollection<OrderItem> orderItems);
}
