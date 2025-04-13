namespace UserService.Models;

public class LoginResponse {
	public string UserName { get; set; }
	public string PhoneNumber { get; set; }
	public string Email { get; set; }
	public ICollection<string> Roles { get; set; }
	public int EmployeeShop { get; set; }
	public string AvatarUser { get; set; }
}

