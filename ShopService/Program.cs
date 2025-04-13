using Mapster;
using Scalar.AspNetCore;
using ShopService.Context;
using ShopService.Repositories;
using ShopService.Repositories.Interfaces;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddMapster();
builder.Services.AddMediator(options => {
	options.ServiceLifetime = ServiceLifetime.Scoped;
});
builder.AddNpgsqlDbContext<ShopDbContext>("ShopDatabase");
builder.Services.AddScoped<IShopRepository, ShopRepository>();
var app = builder.Build();

app.MapDefaultEndpoints();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.MapScalarApiReference();
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
