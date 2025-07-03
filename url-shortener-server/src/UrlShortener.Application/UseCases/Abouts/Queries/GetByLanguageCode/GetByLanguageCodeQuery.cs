using UrlShortener.Application.Dtos;
using UrlShortener.Application.Interfaces.Messaging;

namespace UrlShortener.Application.UseCases.Abouts.Queries.GetByLanguageCode;

public record GetByLanguageCodeQuery(string LanguageCode) : IQuery<AboutDto>;