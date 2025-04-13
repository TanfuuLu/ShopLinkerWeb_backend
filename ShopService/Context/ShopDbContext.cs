using Microsoft.EntityFrameworkCore;
using ShopService.Models;

namespace ShopService.Context;

public class ShopDbContext : DbContext {
	public ShopDbContext(DbContextOptions<ShopDbContext> options) : base(options) {
	}
	public DbSet<Shop> Shops { get; set; }
}
