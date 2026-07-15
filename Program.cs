
using PhotoStore.Application.Interfaces;
using PhotoStore.Infrastructure.Services;
using PhotoStore.Middleware;
using PhotoStore.Infrastructure;
using PhotoStore.Application;

var builder = WebApplication.CreateBuilder(args);

builder.WebHost.UseUrls("http://localhost:5098", "https://localhost:7113");

builder.Services.AddApplication();

builder.Services.AddInfrastructure(builder.Configuration);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseMiddleware<ExceptionMiddleware>();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseSwagger();
app.UseSwaggerUI();

app.UseAuthorization();

app.MapControllers();

app.Run();