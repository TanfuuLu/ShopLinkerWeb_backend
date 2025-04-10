using UserService.Models;

namespace UserService.Repositories.Interfaces;

public interface IUserRepository {
	Task<dynamic> Login(LoginDTO model);
	Task<ApplicationUser> Register(RegisterDTO model);
	Task Logout();
	Task ForgetPassword(string email);
	Task<ApplicationUser> ChangePassword(ChangePasswordDTO model);
	Task<ApplicationUser> GetUserByIdAsync(Guid userId);
	Task<ApplicationUser> GetUserByEmailAsync(string email);
	Task UpdateUserAsync(ApplicationUser user);
}
