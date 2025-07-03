using MediatR;
using UrlShortener.Api.Abstractions;
using UrlShortener.Api.Consts;
using UrlShortener.Application.UseCases.Abouts.Queries.GetByLanguageCode;

namespace UrlShortener.Api.Endpoints.About.GetAboutByLanguageCode;

public class GetByLanguageCodeEndpoint : BaseEndpoint, IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("api/about/{languageCode}", async (string languageCode, ISender sender) =>
            {
                var query = new GetByLanguageCodeQuery(languageCode);
                var result = await sender.Send(query);

                return result.IsFailure
                    ? Results.NotFound(result.Error)
                    : Results.Ok(result.Value);
            })
            .WithTags(EndpointTags.About)
            .AllowAnonymous();
    }
}