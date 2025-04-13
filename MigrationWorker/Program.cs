using MigrationWorker;
using ShopService.Context;
using UserService.Context;

var builder = Host.CreateApplicationBuilder(args);

builder.AddServiceDefaults();
builder.Services.AddHostedService<Worker>();
builder.Services.AddOpenTelemetry().WithTracing(trace => trace.AddSource(nameof(Worker)));
builder.AddNpgsqlDbContext<UserDbContext>("UserDatabase");
builder.AddNpgsqlDbContext<ShopDbContext>("ShopDatabase");
var host = builder.Build();
host.Run();
