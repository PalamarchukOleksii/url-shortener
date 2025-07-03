using UrlShortener.Application.Interfaces.Messaging;
using UrlShortener.Domain.Interfaces.Repositories;
using UrlShortener.Domain.Models.AboutModel;
using UrlShortener.Domain.Shared;

namespace UrlShortener.Application.UseCases.Abouts.Queries.GetAboutByLanguageCode;

public class GetAboutByLanguageCodeQueryHandler(IAboutRepository aboutRepository) : IQueryHandler<GetAboutByLanguageCodeQuery, string>
{
    public async Task<Result<string>> Handle(GetAboutByLanguageCodeQuery request, CancellationToken cancellationToken)
    {
        if (!Enum.TryParse<LanguageCode>(request.LanguageCode, ignoreCase: true, out var languageCode))
        {
            return Result.Failure<string>(new Error(
                "About.InvalidLanguageCode",
                $"Invalid language code: '{request.LanguageCode}'."));
        }
        
        var about = await aboutRepository.GetByLanguageCode(languageCode); 
        if (about == null)
        {
            return Result.Failure<string>(new Error(
                "About.NotFound",
                $"About description not found for language '{request.LanguageCode}'."));
        }
        
        return Result.Success(about.Description);
    }
}