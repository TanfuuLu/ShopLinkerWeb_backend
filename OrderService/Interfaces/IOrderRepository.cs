using OrderService.Models;

namespace OrderService.Interfaces;

public interface IOrderRepository {
	Task<bool> CreateOrderAsync(Order order);
	Task<bool> DeleteOrderAsync(int id);
	Task<bool> DeleteOrderItemFromOrderAsync(int orderItemId, int orderID);
	Task<Order> GetOrderByIdAsync(int id);
	Task<ICollection<Order>> GetAllOrdersAsync();
	Task<ICollection<Order>> GetOrdersByShopIdAsync(int shopId);
	Task<ICollection<Order>> GetOrdersByOrderTimeAsync(string orderTime);
	Task<ICollection<Order>> GetOrdersByFilterAsync(string orderTime, int shopID);
}
