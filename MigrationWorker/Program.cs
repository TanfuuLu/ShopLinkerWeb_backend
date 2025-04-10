using MigrationWorker;
using UserService.Context;

var builder = Host.CreateApplicationBuilder(args);

builder.AddServiceDefaults();
builder.Services.AddHostedService<Worker>();
builder.Services.AddOpenTelemetry().WithTracing(trace => trace.AddSource(nameof(Worker)));
builder.AddNpgsqlDbContext<UserDbContext>("UserDatabase");
var host = builder.Build();
host.Run();
