namespace UserService.Models;

public class ChangePasswordDTO {
	public string Email { get; set; }
	public string VerifyCode { get; set; }
	public string NewPassword { get; set; }
}
