using Microsoft.EntityFrameworkCore;
using OrderService.Models;

namespace OrderService.Context;

public class OrderDbContext : DbContext {
	public OrderDbContext(DbContextOptions<OrderDbContext> options) : base(options) {
	}

	public DbSet<OrderItem> OrderItem { get; set; }
	public DbSet<Order> Orders { get; set; }
}
