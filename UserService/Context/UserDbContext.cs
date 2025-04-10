using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;
using UserService.Models;

namespace UserService.Context;

public class UserDbContext : IdentityDbContext<ApplicationUser> {
	public UserDbContext(DbContextOptions<UserDbContext> options) : base(options) {
	}
	public DbSet<PasswordResetRequest> PasswordResetNumber { get; set; }
	protected override void OnModelCreating(ModelBuilder builder) {
		base.OnModelCreating(builder);
		var adminRoleId = "AdminId";
		var userRoleId = "UserId";
		builder.Entity<IdentityRole>().HasData(
			new IdentityRole {
				Id = adminRoleId.ToString(),
				Name = "Admin",
				NormalizedName = "ADMIN"
			},
			new IdentityRole {
				Id = userRoleId.ToString(),
				Name = "User",
				NormalizedName = "USER"
			}
		);
	}
}
