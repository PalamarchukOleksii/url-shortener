using UrlShortener.Application.Interfaces.Messaging;

namespace UrlShortener.Application.UseCases.ShortenedUrls.Queries.RedirectToOriginalUrl;

public record RedirectToOriginalUrlQuery(string ShortCode) : IQuery<string>;