var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Projects.ApiGateway>("apigateway");

builder.AddProject<Projects.BlazorApp>("blazorapp");

builder.AddProject<Projects.Feedback_Api>("feedback-api");

builder.AddProject<Projects.Notification_Api>("notification-api");

builder.AddProject<Projects.Order_Api>("order-api");

builder.AddProject<Projects.Product_Api>("product-api");

builder.AddProject<Projects.User_Api>("user-api");

builder.Build().Run();
