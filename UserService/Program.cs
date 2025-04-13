using Mapster;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Scalar.AspNetCore;
using UserService.Context;
using UserService.Models;
using UserService.Repositories;
using UserService.Repositories.Interfaces;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddScoped<IEmailService, EmailRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<JwtTokenService>();
builder.AddNpgsqlDbContext<UserDbContext>("UserDatabase");
builder.Services.AddMapster();
builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options => {
	options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(10);
	options.Lockout.MaxFailedAccessAttempts = 3;
	options.Lockout.AllowedForNewUsers = true;
})
	.AddEntityFrameworkStores<UserDbContext>()
	.AddDefaultTokenProviders();
builder.Services.AddAuthentication(options => {
	options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
	options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
	.AddJwtBearer(options => {
		options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters {
			ValidateIssuer = true,
			ValidateAudience = true,
			ValidateLifetime = true,
			ValidateIssuerSigningKey = true,
			ValidIssuer = builder.Configuration["Jwt:Issuer"],
			ValidAudience = builder.Configuration["Jwt:Issuer"],
			IssuerSigningKey = new Microsoft.IdentityModel.Tokens.SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
		};
		options.Events = new JwtBearerEvents {
			OnMessageReceived = context => {
				var token = context.Request.Cookies["JwtToken"];
				if(!string.IsNullOrEmpty(token)) {
					context.Token = token;
				}
				return Task.CompletedTask;
			}
		};
	});
builder.Services.Configure<IdentityOptions>(options => {
	options.Password.RequireDigit = false;
	options.Password.RequiredLength = 1;
	options.Password.RequireLowercase = false;
	options.Password.RequireNonAlphanumeric = false;
	options.Password.RequireUppercase = false;
	options.Password.RequiredUniqueChars = 0;
});
builder.Services.AddCors(options => {
	options.AddPolicy("AllowAllOrigins",
		builder => {
			builder.WithOrigins("http://localhost:4200")
				.AllowAnyMethod()
				.AllowAnyHeader();
		});
});



var app = builder.Build();

app.MapDefaultEndpoints();

// Configure the HTTP request pipeline.
if(app.Environment.IsDevelopment()) {
	app.MapScalarApiReference();
	app.MapOpenApi();
}
app.UseCors("AllowAllOrigins");	
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
