namespace UrlShortener.Api.Abstractions;

public interface IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app);
}