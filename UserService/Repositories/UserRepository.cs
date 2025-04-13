using MapsterMapper;
using Microsoft.AspNetCore.Identity;
using System.Net;
using UserService.Context;
using UserService.Models;
using UserService.Repositories.Interfaces;

namespace UserService.Repositories;

public class UserRepository : IUserRepository {
	private readonly UserManager<ApplicationUser> userManager;
	private readonly SignInManager<ApplicationUser> signInManager;
	private readonly IEmailService emailService;
	private readonly JwtTokenService jwtTokenService;
	private readonly IHttpContextAccessor HttpResponse;
	private readonly UserDbContext dbContext;
	private readonly IMapper mapper;

	public UserRepository(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IEmailService emailService, JwtTokenService jwtTokenService, IHttpContextAccessor httpContextAccessor, UserDbContext dbContext, IMapper mapper) {
		this.userManager = userManager;
		this.signInManager = signInManager;
		this.emailService = emailService;
		this.jwtTokenService = jwtTokenService;
		this.HttpResponse = httpContextAccessor;
		this.dbContext = dbContext;
		this.mapper = mapper;
	}
	public async Task ForgetPassword(string email) {
		var userExisted = await userManager.FindByEmailAsync(email);
		if(userExisted == null) {
			throw new Exception("User not found");
		}
		Random rand = new Random();
		var shortToken = rand.Next(1,1000000).ToString();
		var IdentityToken = await userManager.GeneratePasswordResetTokenAsync(userExisted);
		var request = new PasswordResetRequest {
			Email = email,
			ShortTokenNumber = shortToken,
			IdentityToken = IdentityToken,
			ExpireAt = DateTime.UtcNow.AddMinutes(30)
		};
		await dbContext.PasswordResetNumber.AddAsync(request);
		await dbContext.SaveChangesAsync();
		await emailService.SendEmailAsync(email, "Reset Pasword Code", $"<p>Reset Password Code:</p> </br> <h1>{shortToken}</h1>" );


	}

	public async Task<ApplicationUser> ChangePassword(ChangePasswordDTO model) {
		var user = await userManager.FindByEmailAsync(model.Email);
		if(user == null) {
			throw new Exception("User not found");
		}
		var requestCheck = dbContext.PasswordResetNumber.FirstOrDefault(x => x.Email == model.Email && x.ShortTokenNumber == model.VerifyCode);
		if(requestCheck == null) {
			throw new Exception("Invalid short code");
		}
		if(requestCheck.ExpireAt < DateTime.UtcNow) {
			throw new Exception("Short code expired");
		}
		var result = await userManager.ResetPasswordAsync(user, requestCheck.IdentityToken, model.NewPassword);
		if(!result.Succeeded) {
			throw new Exception("Failed to reset password");
		}
		dbContext.PasswordResetNumber.Remove(requestCheck);
		await dbContext.SaveChangesAsync();
		var userExisted = await userManager.FindByEmailAsync(model.Email);
		return userExisted;

	}
	public async Task<ApplicationUser> GetUserByEmailAsync(string email) {
		var userExisted = await userManager.FindByEmailAsync(email);
		if(userExisted == null) {
			return null;
		}
		return userExisted;
	}

	public async Task<dynamic> Login(LoginDTO model) {
		var userExistes = await userManager.FindByEmailAsync(model.Email);
		if(userExistes == null) {
			return "Not Found Account";
		}
		if(await userManager.IsLockedOutAsync(userExistes)) {
			var lockoutEnd = await userManager.GetLockoutEndDateAsync(userExistes);
			var timeCheck = (lockoutEnd - DateTimeOffset.UtcNow)?.Minutes;
			return $"User is locked out, Please try again in {timeCheck}";
		}
		var result = await signInManager.CheckPasswordSignInAsync(userExistes, model.Password, lockoutOnFailure: true);
		if(!result.Succeeded) {
			if(await userManager.GetAccessFailedCountAsync(userExistes) > 3) {
				await userManager.SetLockoutEndDateAsync(userExistes, DateTimeOffset.UtcNow.AddMinutes(10));
				return "User is locked out, Please try again in 10 minutes";
			}
			return"Wrong Password";
		}
		await userManager.ResetAccessFailedCountAsync(userExistes);
		var userRoles = await userManager.GetRolesAsync(userExistes);
		var token = jwtTokenService.GenerateToken(userExistes, userRoles);
		var cookieOptions = new CookieOptions {
			HttpOnly = true,
			Secure = true,
			SameSite = SameSiteMode.Strict,
			Expires = DateTimeOffset.UtcNow.AddDays(1)
		};
		HttpResponse.HttpContext.Response.Cookies.Append("JwtToken", token, cookieOptions);
		var resultMaped = mapper.Map<LoginResponse>(userExistes);
		resultMaped.Roles = userRoles;
		return new {
			token,
			resultMaped
		};
	}

	public async Task<ApplicationUser> Register(RegisterDTO model) {
		var user = new ApplicationUser {
			UserName = model.Username,
			Email = model.Email,
			PhoneNumber = model.Phonenumber,
			EmailConfirmed = true,
			EmployeeStore = model.EmployeeStore,
		};
		var result = await userManager.CreateAsync(user, model.Password);
		if(!result.Succeeded) {
			return null;
		}
		if(model.Role == "Admin") {
			await userManager.AddToRoleAsync(user, "Admin");
		} else if(model.Role == ""){
			await userManager.AddToRoleAsync(user, "User");
		}
		
		var userCreated = await userManager.FindByEmailAsync(model.Email);
		return user;
	}
	public Task Logout() {
		HttpResponse.HttpContext.Response.Cookies.Delete("JwtToken");
		return Task.CompletedTask;
	}
	public Task UpdateUserAsync(ApplicationUser user) {
		throw new NotImplementedException();
	}

	public async Task<ICollection<ApplicationUser>> GetListUserById(List<int> listId) {
		if(listId == null || listId.Count == 0) {
			return null;
		}
		ICollection<ApplicationUser> result = new List<ApplicationUser>();
		foreach(var id in listId) {
			var user = await userManager.FindByIdAsync(id.ToString());
			if(user != null) {
				result.Add(user);
			}
		}
		if(result.Count == 0) {
			return null;
		}
		return result;
	}
}
