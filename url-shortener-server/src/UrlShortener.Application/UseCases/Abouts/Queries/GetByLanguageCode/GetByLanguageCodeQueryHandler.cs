using UrlShortener.Application.Dtos;
using UrlShortener.Application.Interfaces.Messaging;
using UrlShortener.Domain.Interfaces.Repositories;
using UrlShortener.Domain.Models.AboutModel;
using UrlShortener.Domain.Shared;

namespace UrlShortener.Application.UseCases.Abouts.Queries.GetByLanguageCode;

public class GetByLanguageCodeQueryHandler(IAboutRepository aboutRepository) : IQueryHandler<GetByLanguageCodeQuery, AboutDto>
{
    public async Task<Result<AboutDto>> Handle(GetByLanguageCodeQuery request, CancellationToken cancellationToken)
    {
        if (!Enum.TryParse<LanguageCode>(request.LanguageCode, ignoreCase: true, out var languageCode))
        {
            return Result.Failure<AboutDto>(new Error(
                "About.InvalidLanguageCode",
                $"Invalid language code: '{request.LanguageCode}'."));
        }
        
        var about = await aboutRepository.GetByLanguageCode(languageCode); 
        if (about == null)
        {
            return Result.Failure<AboutDto>(new Error(
                "About.NotFound",
                $"About description not found for language '{request.LanguageCode}'."));
        }

        var aboutDto = new AboutDto
        {
            Id = about.Id,
            Description = about.Description,
            Language = about.Language.ToString(),
            LastEditAt = about.LastEditAt,
        };
        
        return Result.Success(aboutDto);
    }
}