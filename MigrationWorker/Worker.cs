using EmployeeService.Context;
using Microsoft.EntityFrameworkCore;
using OpenTelemetry.Trace;
using ShopService.Context;
using System.Diagnostics;
using UserService.Context;

namespace MigrationWorker;

public class Worker : BackgroundService
{
	private readonly IServiceProvider serviceProvider;
	private readonly IHostApplicationLifetime hostApplicationLifetime;
	public const string ActivityName = "Migration";
	private static readonly ActivitySource ActivitySource = new(ActivityName);

	public Worker(IServiceProvider serviceProvider, IHostApplicationLifetime hostApplicationLifetime) {
		this.serviceProvider = serviceProvider;
		this.hostApplicationLifetime = hostApplicationLifetime;

	}
	protected override async Task ExecuteAsync(CancellationToken stoppingToken) {
		using var activity = ActivitySource.StartActivity("Migration", ActivityKind.Client);
		try {
			using var scope = serviceProvider.CreateScope();
			var UserDb = scope.ServiceProvider.GetRequiredService<UserDbContext>();
			var ShopDb = scope.ServiceProvider.GetRequiredService<ShopDbContext>();
			var EmployeeDb = scope.ServiceProvider.GetRequiredService<EmployeeDbContext>();
			await EnsureDatabaseAsync(ShopDb, stoppingToken);
			await EnsureDatabaseAsync(UserDb, stoppingToken);
			await EnsureDatabaseAsync(EmployeeDb, stoppingToken);

		}
		catch(Exception ex) {
			activity?.RecordException(ex);
			throw;
		}
		hostApplicationLifetime.StopApplication();
	}
	public static async Task EnsureDatabaseAsync(DbContext dbContext, CancellationToken token) {
		if(!await dbContext.Database.CanConnectAsync(token)) {
			await dbContext.Database.MigrateAsync(token);
		}
	}
}
