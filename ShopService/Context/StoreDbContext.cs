using Microsoft.EntityFrameworkCore;
using ShopService.Models;

namespace ShopService.Context;

public class StoreDbContext : DbContext {
	public StoreDbContext(DbContextOptions<StoreDbContext> options) : base(options) {
	}
	public DbSet<Store> Stores { get; set; }
}
