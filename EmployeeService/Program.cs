using EmployeeService.Context;
using EmployeeService.Interfaces;
using EmployeeService.Repositories;
using Mapster;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddMediator(options => {
	options.ServiceLifetime = ServiceLifetime.Scoped;
});
builder.Services.AddMapster();
builder.AddNpgsqlDbContext<EmployeeDbContext>("EmployeeDatabase");
builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();

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
