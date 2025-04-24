using EmployeeService.Models;
using Microsoft.EntityFrameworkCore;

namespace EmployeeService.Context;

public class EmployeeDbContext : DbContext {
	public EmployeeDbContext(DbContextOptions<EmployeeDbContext> options) : base(options) {
	}
	public DbSet<Employee> Employees { get; set; }
	protected override void OnModelCreating(ModelBuilder modelBuilder) {
		base.OnModelCreating(modelBuilder);
		modelBuilder.Entity<Employee>()
			.Property(p => p.StarterDate)
			.HasDefaultValueSql("NOW()");
	}
}
