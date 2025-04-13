using Microsoft.AspNetCore.Identity;

namespace UserService.Models;

public class ApplicationUser : IdentityUser{
	public string AvatarUser { get; set; } = string.Empty;
	public int EmployeeShop { get; set; }
}
