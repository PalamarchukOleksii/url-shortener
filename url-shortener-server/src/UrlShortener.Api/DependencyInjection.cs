using System.Reflection;
using UrlShortener.Api.Extensions;

namespace UrlShortener.Api;

public static class DependencyInjection
{
    public static IServiceCollection AddApi(this IServiceCollection services)
    {
        services.AddControllers();
        services.AddOpenApi();
        
        services.AddEndpoints(Assembly.GetExecutingAssembly());

        return services;
    }
}