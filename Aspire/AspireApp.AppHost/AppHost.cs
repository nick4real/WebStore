var builder = DistributedApplication.CreateBuilder(args);

var sqlServer = builder.AddSqlServer("sqlServer").WithLifetime(ContainerLifetime.Persistent);

var userDatabase = sqlServer.AddDatabase("userDatabase");
var userService = builder.AddProject<Projects.WebStoreUser_API>("userService")
    .WaitFor(userDatabase)
    .WithReference(userDatabase, "UserDatabase");

var blazorWebApp = builder.AddProject<Projects.BlazorApp>("blazorWebApp");

var webStoreGateway = builder.AddProject<Projects.WebStoreGateway>("webStoreGateway")
    .WithReference(blazorWebApp)
    .WithReference(userService);

builder.Build().Run();
