var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Projects.WebStoreGateway>("webstoregateway");

builder.AddProject<Projects.BlazorApp>("blazorapp");

builder.AddProject<Projects.WebStoreFeedback_API>("webstorefeedback-api");

builder.AddProject<Projects.WebStoreNotification_API>("webstorenotification-api");

builder.AddProject<Projects.WebStoreOrder_API>("webstoreorder-api");

builder.AddProject<Projects.WebStoreProduct_API>("webstoreproduct-api");

builder.AddProject<Projects.WebStoreUser_API>("webstoreuser-api");

builder.Build().Run();
