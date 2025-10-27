using Scalar.AspNetCore;
using Serilog;
using TechnicalTask.BLL.Middleware;
using TechnicalTask.WebApi.Extentions;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((ctx, lc) =>
{
    lc.WriteTo.Console();
});


builder.Services.AddControllers();
builder.Services.AddOpenApi();


builder.Services.AddApplicationServices(builder.Configuration);
builder.Services.AddCustomServices();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseMiddleware<RateLimitingMiddleware>(10, TimeSpan.FromSeconds(1));

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
