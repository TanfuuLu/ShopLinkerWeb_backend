﻿namespace UserService.Models;

public class RegisterDTO {
	public string Username { get; set; }
	public string Phonenumber { get; set; }
	public string Email { get; set; }
	public string Password { get; set; }
	public string Role { get; set; } = "User";
	public int EmployeeShop { get; set; }
}
