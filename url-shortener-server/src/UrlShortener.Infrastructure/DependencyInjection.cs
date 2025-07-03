using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using UrlShortener.Application.Interfaces.Data;
using UrlShortener.Application.Interfaces.Security;
using UrlShortener.Application.Interfaces.Services;
using UrlShortener.Domain.Interfaces.Repositories;
using UrlShortener.Infrastructure.Data;
using UrlShortener.Infrastructure.Repositories;
using UrlShortener.Infrastructure.Security;
using UrlShortener.Infrastructure.Services;

namespace UrlShortener.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>(options => 
            options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));
        
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IRoleRepository, RoleRepository>();
        services.AddScoped<IUserRoleRepository, UserRoleRepository>();
        services.AddScoped<IShortenedUrlRepository, ShortenedUrlRepository>();
        
        services.AddScoped<IUrlShortenerService, UrlShortenerService>();
        
        services.AddSingleton<IHasher, Hasher>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        
        return services;
    }
}