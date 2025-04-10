using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using UserService.Models;

namespace UserService.Repositories;

public class JwtTokenService {
	private readonly IConfiguration _configuration;

	public JwtTokenService(IConfiguration configuration) {
		_configuration = configuration;
	}

	public string GenerateToken(ApplicationUser user, IList<string> roles) {
		var claims = new List<Claim>
		{
		new Claim(ClaimTypes.NameIdentifier, user.Id),
		new Claim(ClaimTypes.Name, user.UserName),
		new Claim(ClaimTypes.Email, user.Email)
	  };
		claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));

		var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
		var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

		var token = new JwtSecurityToken(
		    issuer: _configuration["Jwt:Issuer"],
		    audience: _configuration["Jwt:Audience"],
		    claims: claims,
		    expires: DateTime.Now.AddDays(1),
		    signingCredentials: creds);

		return new JwtSecurityTokenHandler().WriteToken(token);
	}
}
