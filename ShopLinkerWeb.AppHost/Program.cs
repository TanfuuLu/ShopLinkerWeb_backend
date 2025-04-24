var builder = DistributedApplication.CreateBuilder(args);

var VoiUsername = builder.AddParameter("DbUsername");
var VoiPassword = builder.AddParameter("DbPassword", secret: true);
var PostgreConfig = builder.AddPostgres("PostgreSQL", userName: VoiUsername, password: VoiPassword, port: 8001)
			.WithImage("postgres")
			.WithPgAdmin()
			.WithDataVolume("ShopLinker", isReadOnly: false);
var RedisConfig = builder.AddRedis("Redis")
			.WithImage("redis");
var ThoUsername = builder.AddParameter("UsernameRabbit");
var ThoPassword = builder.AddParameter("PasswordRabbit", secret: true);
var RabbitConfig = builder.AddRabbitMQ("RabbitMQ", userName: ThoUsername, password: ThoPassword, port: 5672)
			.WithManagementPlugin();

var UserDatabase = PostgreConfig.AddDatabase("UserDatabase");
var ShopDatabase = PostgreConfig.AddDatabase("ShopDatabase");
var EmployeeDatabase = PostgreConfig.AddDatabase("EmployeeDatabase");

var ShopService = builder.AddProject<Projects.UserService>("userservice")
	.WithReference(UserDatabase)
	.WaitFor(UserDatabase)
	.WithEndpoint("https", endpoint => endpoint.IsProxied = false);
var UserSerivce = builder.AddProject<Projects.ShopService>("shopservice")
	.WithReference(ShopDatabase)
	.WithReference(RedisConfig)
	.WaitFor(ShopDatabase)
	.WithEndpoint("https", endpoint => endpoint.IsProxied = false);
builder.AddProject<Projects.MigrationWorker>("migrationworker")
		.WithReference(UserDatabase)
		.WithReference(ShopDatabase)
		.WithReference(EmployeeDatabase)
		.WaitFor(EmployeeDatabase)
		.WaitFor(UserDatabase)
		.WaitFor(ShopDatabase);


builder.AddProject<Projects.ApiGatewayService>("apigatewayservice")
	.WithReference(UserSerivce)
	.WithReference(ShopService)
	.WithEndpoint("https", endpoint => endpoint.IsProxied = false);
builder.AddProject<Projects.EmployeeService>("employeeservice")
	.WithReference(EmployeeDatabase)
	.WithReference(RedisConfig)
	.WithEndpoint("https", endpoint => endpoint.IsProxied = false);

builder.Build().Run();
