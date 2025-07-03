using Microsoft.AspNetCore.Mvc;
using UrlShortener.Application.UseCases.ShortenedUrls.Queries.RedirectToOriginalUrl;

namespace UrlShortener.Api.Controllers;

[ApiController]
[Route("")]
public class RedirectController(RedirectToOriginalUrlQueryHandler redirectToOriginalUrlQueryHandler) :ControllerBase
{
    [HttpGet("{shortCode}")]
    public async Task<IActionResult> RedirectToOriginalUrl(string shortCode, CancellationToken cancellationToken)
    {
        var result = await redirectToOriginalUrlQueryHandler.Handle(new RedirectToOriginalUrlQuery(shortCode), cancellationToken);

        if (result.IsFailure)
        {
            return NotFound(result.Error);
        }
        
        return Redirect(result.Value);
    }
}