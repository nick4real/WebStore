using Scalar.AspNetCore;
using WebStoreUser.Infrastructure;

// Builder
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddInfrastructure(builder.Configuration);

builder.AddServiceDefaults();
builder.Services.AddControllers();
builder.Services.AddOpenApi();

// App
var app = builder.Build();

app.MapDefaultEndpoints();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
