using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;
using TechnicalTask.BLL;
using TechnicalTask.DAL.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<TechnicalTaskDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssembly(typeof(BLLAsemblyMarker).Assembly);
    //cfg.AddOpenBehavior(typeof(ValidationBehaviour<,>));
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
