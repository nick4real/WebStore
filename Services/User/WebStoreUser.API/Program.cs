using Scalar.AspNetCore;
using WebStoreUser.Infrastructure;
using WebStoreUser.Infrastructure.Filters;
using WebStoreUser.Infrastructure.Options;

// Builder
var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<JwtOptions>(builder.Configuration.GetSection("JwtOptions"));

builder.Services.AddInfrastructure(builder.Configuration);

builder.AddServiceDefaults();
builder.Services.AddControllers(options =>
{
    options.Filters.Add<ValidationFilter>();
});
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
