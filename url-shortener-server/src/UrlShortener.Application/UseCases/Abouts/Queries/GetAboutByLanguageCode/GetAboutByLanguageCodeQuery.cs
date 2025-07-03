using UrlShortener.Application.Dtos;
using UrlShortener.Application.Interfaces.Messaging;
using UrlShortener.Domain.Models.AboutModel;

namespace UrlShortener.Application.UseCases.Abouts.Queries.GetAboutByLanguageCode;

public record GetAboutByLanguageCodeQuery(string LanguageCode) : IQuery<AboutDto>;