using UrlShortener.Domain.Models.AboutModel;

namespace UrlShortener.Api.Endpoints.About.Update;

public record UpdateRequest(AboutId Id,string Description);