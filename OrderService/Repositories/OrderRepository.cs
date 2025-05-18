
using Microsoft.EntityFrameworkCore;
using OrderService.Context;
using OrderService.Interfaces;
using OrderService.Models;

namespace OrderService.Repositories;

public class OrderRepository : IOrderRepository {
	private readonly OrderDbContext context;
	public OrderRepository(OrderDbContext context) {
		this.context = context;
	}
	public async Task<bool> CreateOrderAsync(Order order) {
		await context.Orders.AddAsync(order);
		var result = await context.SaveChangesAsync();
		if (result > 1) {
			return true;
		}
		else {
			return false;
		}
	}

	public async Task<bool> DeleteOrderAsync(int id) {
		var checkItem = await context.Orders.FirstOrDefaultAsync(x => x.OrderID == id);
		if (checkItem == null) {
			return false;
		}
		context.Orders.Remove(checkItem);
		var result = await context.SaveChangesAsync();
		if (result > 1) {
			return true;
		}
		else {
			return false;
		}
	}

	public async Task<bool> DeleteOrderItemFromOrderAsync(int orderItemID, int orderID) {
		var orderExisted = await context.Orders
			.Include(x => x.OrderItems)
			.FirstOrDefaultAsync(x => x.OrderID == orderID);
		if (orderExisted == null) {
			return false;
		}
		if(orderExisted.OrderItems.FirstOrDefault(c => c.OrderItemID == orderItemID) == null) {
			return false;
		}
		else {
			orderExisted.OrderItems.Remove(orderExisted.OrderItems.FirstOrDefault(x => x.OrderItemID == orderItemID));
			await context.SaveChangesAsync();
			return true;
		}
	}

	public async Task<ICollection<Order>> GetAllOrdersAsync() {
		var listOrder = await context.Orders
			.Include(x => x.OrderItems)
			.ToListAsync();
		if (listOrder == null) {
			return new List<Order>();
		}
		else {
			return listOrder;
		}
	}

	public async Task<Order> GetOrderByIdAsync(int id) {
		var checkOrder = await context.Orders.FirstOrDefaultAsync(x => x.OrderID == id);
		if (checkOrder == null) {
			return null;
		}
		else {
			return checkOrder;
		}
	}

	public async Task<ICollection<Order>> GetOrdersByFilterAsync(string orderTime, int shopID) {
		var listOrder = await context.Orders.Where(x => x.OrderTime == orderTime && x.ShopID == shopID)
			.Include(x => x.OrderItems)
			.ToListAsync();
		if (listOrder == null) {
			return null;
		}
		else {
			return listOrder;
		}
	}
	public async Task<ICollection<Order>> GetOrdersByOrderTimeAsync(string orderTime) {
		var listOrder = await context.Orders.Where(x => x.OrderTime == orderTime)
			.Include(x => x.OrderItems)
			.ToListAsync();
		if (listOrder == null) {
			return new List<Order>();
		}
		else {
			return listOrder;
		}
	}

	public async Task<ICollection<Order>> GetOrdersByShopIdAsync(int shopId) {
		var listOrder = await context.Orders.Where(x => x.ShopID == shopId)
			.Include(x => x.OrderItems)
			.ToListAsync();
		if (listOrder == null) {
			return new List<Order>();
		}
		else {
			return listOrder;
		}
	}

}
