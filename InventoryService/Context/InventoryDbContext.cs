using InventoryService.Models;
using Microsoft.EntityFrameworkCore;

namespace InventoryService.Context;

public class InventoryDbContext : DbContext {
	public InventoryDbContext(DbContextOptions<InventoryDbContext> options) : base(options) {
	}

	public DbSet<InventoryItem> InventoryItems { get; set; } 
	public DbSet<Category> Categories { get; set; }
}
