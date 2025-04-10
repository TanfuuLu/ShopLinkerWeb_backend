namespace UserService.Models;

public class PasswordResetRequest {
	public Guid Id { get; set; } = Guid.NewGuid();
	public string? Email { get; set; }
	public string? ShortTokenNumber { get; set; } 
	public string? IdentityToken { get; set; } 
	public DateTime ExpireAt { get; set; }
}
