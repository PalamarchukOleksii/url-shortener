using System.Reflection;
using Microsoft.AspNetCore.Authentication.Cookies;
using UrlShortener.Api.Extensions;

namespace UrlShortener.Api;

public static class DependencyInjection
{
    public static IServiceCollection AddApi(this IServiceCollection services)
    {
        services.AddControllers();
        services.AddOpenApi();

        services.AddEndpoints(Assembly.GetExecutingAssembly());

        services.AddDistributedMemoryCache();
        services.AddSession(options =>
        {
            options.IdleTimeout = TimeSpan.FromMinutes(30);
            options.Cookie.HttpOnly = true;
            options.Cookie.IsEssential = true;
        });

        services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            .AddCookie(options =>
            {
                options.ExpireTimeSpan = TimeSpan.FromMinutes(30);
                options.SlidingExpiration = true;

                options.Events.OnRedirectToLogin = context =>
                {
                    if (context.Request.Path.StartsWithSegments("/api"))
                    {
                        context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                        return Task.CompletedTask;
                    }

                    context.Response.Redirect(context.RedirectUri);
                    return Task.CompletedTask;
                };

                options.Events.OnRedirectToAccessDenied = context =>
                {
                    if (context.Request.Path.StartsWithSegments("/api"))
                    {
                        context.Response.StatusCode = StatusCodes.Status403Forbidden;
                        return Task.CompletedTask;
                    }

                    context.Response.Redirect(context.RedirectUri);
                    return Task.CompletedTask;
                };
            });


        services.AddAuthorizationBuilder()
            .AddPolicy("AdminOnly", policy => policy.RequireRole("Admin"))
            .AddPolicy("UserOrAdmin", policy => policy.RequireRole("User", "Admin"));

        return services;
    }
}