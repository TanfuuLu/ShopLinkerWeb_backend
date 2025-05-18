using Microsoft.EntityFrameworkCore;
using OrderService.Context;
using OrderService.Interfaces;
using OrderService.Models;

namespace OrderService.Repositories;

public class OrderItemRepository : IOrderItemRepository {
	private readonly OrderDbContext context;

	public OrderItemRepository(OrderDbContext context) {
		this.context = context;
	}

	public async Task<bool> CreateOrderItemAsync(OrderItem orderItem) {
		var totalPrice = orderItem.Quantity * orderItem.Price;
		orderItem.TotalPrice = totalPrice;
		var createdOrderItem = await context.OrderItem.AddAsync(orderItem);
		var result = await context.SaveChangesAsync();
		if (result > 1) {
			return true;
		}
		else {
			return false;

		}
	}

	public async Task<bool> CreateListOrderItemsAsync(ICollection<OrderItem> orderItems) {
		foreach(var item in orderItems) {
			var totalPrice = item.Quantity * item.Price;
			item.TotalPrice = totalPrice;
		}
		await context.OrderItem.AddRangeAsync(orderItems);
		var result = await context.SaveChangesAsync();
		if (result > 1) {
			return true;
		}
		else {
			return false;
		}
	}

	public async Task<ICollection<OrderItem>> GetOrderItemsOfOrderAsync(int orderId) {
		var listOrderItem = await context.OrderItem
			.Where(x => x.OrderId == orderId)
			.ToListAsync();
		if (listOrderItem == null) {
			return new List<OrderItem>();
		}
		else {
			return listOrderItem;
		}
	}
}
