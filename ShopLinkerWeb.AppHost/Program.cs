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
var StoreDatabase = PostgreConfig.AddDatabase("StoreDatabase");

builder.AddProject<Projects.UserService>("userservice")
	.WithReference(UserDatabase)
	.WaitFor(UserDatabase)
	.WithEndpoint("https", endpoint => endpoint.IsProxied = false);

builder.AddProject<Projects.MigrationWorker>("migrationworker")
		.WithReference(UserDatabase)
		.WaitFor(UserDatabase);

//builder.AddProject<Projects.ShopService>("shopservice")
//	.WithReference(StoreDatabase)
//	.WaitFor(StoreDatabase)
//	.WithEndpoint("https", endpoint => endpoint.IsProxied = false);

builder.Build().Run();
