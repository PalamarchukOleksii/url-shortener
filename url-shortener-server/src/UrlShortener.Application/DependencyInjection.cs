using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace UrlShortener.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        var assembly = Assembly.GetExecutingAssembly();

        assembly.GetTypes()
            .Where(t => t.Name.EndsWith("Handler") && t is { IsAbstract: false, IsInterface: false })
            .ToList()
            .ForEach(t => services.AddTransient(t));
        
        return services;
    }
}