using Microsoft.AspNetCore.Routing;
using UrlShortener.Api.Abstractions;
using UrlShortener.Api.Consts;
using UrlShortener.Domain.Models.AboutModel;
using MediatR;
using UrlShortener.Application.UseCases.Abouts.Queries.GetAboutByLanguageCode;

namespace UrlShortener.Api.Endpoints.About.GetAboutByLanguageCode;

public class GetAboutByLanguageCodeEndpoint : BaseEndpoint, IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("api/about/{languageCode}", async (string languageCode, ISender sender) =>
            {
                var query = new GetAboutByLanguageCodeQuery(languageCode);
                var result = await sender.Send(query);

                return result.IsFailure
                    ? Results.NotFound(result.Error)
                    : Results.Ok(result.Value);
            })
            .WithTags(EndpointTags.About)
            .AllowAnonymous();
    }
}