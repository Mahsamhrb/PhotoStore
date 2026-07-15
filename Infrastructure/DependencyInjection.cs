using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PhotoStore.Application.Interfaces;
using PhotoStore.Infrastructure.Data;
using PhotoStore.Infrastructure.Repositories;
using PhotoStore.Infrastructure.Services;

namespace PhotoStore.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContext<AppDbContext>(options =>
            options.UseNpgsql(
                configuration.GetConnectionString("DefaultConnection")));

        services.AddScoped<IPhotoRepository, PhotoRepository>();

        services.AddScoped<IFileService, FileService>();

        return services;
    }
}