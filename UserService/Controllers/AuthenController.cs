using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using UserService.Models;
using UserService.Repositories;
using UserService.Repositories.Interfaces;

namespace UserService.Controllers;
[Route("api/[controller]")]
[ApiController]
public class AuthenController : ControllerBase {
	private readonly IUserRepository userRepository;

	public AuthenController(IUserRepository userRepository) {
		this.userRepository = userRepository;
	}

	[HttpPost("register")]
	public async Task<IActionResult> RegisterUser([FromBody] RegisterDTO model) {
		var result = await userRepository.Register(model);

		return Ok(result);
	}
	[HttpPost("login")]
	public async Task<IActionResult> LoginUser([FromBody] LoginDTO model) {
		var result = await userRepository.Login(model);
			return Ok(result);
	}
	[HttpPost("forget-password-request")]
	public async Task<IActionResult> ForgetPassword([FromBody] string email) {
		await userRepository.ForgetPassword(email);
		return Ok(new { message = "Reset verify code sent to your email" });
	}
	[HttpPost("reset-password")]
	public async Task<IActionResult> ResetPassword([FromBody] ChangePasswordDTO model) {
		var result = await userRepository.ChangePassword(model);
		return Ok(result);
	}
	[HttpGet("logout")]
	public async Task<IActionResult> Logout() {
		await userRepository.Logout();
		return Ok(new { message = "Logout successfully" });
	}
}
