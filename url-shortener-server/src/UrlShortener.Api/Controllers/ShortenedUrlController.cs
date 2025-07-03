using Microsoft.AspNetCore.Mvc;
using UrlShortener.Api.Dtos.ShortenedUrlDtos;
using UrlShortener.Application.UseCases.ShortenedUrls.Commands.UrlShortening;

namespace UrlShortener.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ShortenedUrlController(ShortenUrlCommandHandler shortenedUrlCommandHandler) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> ShortenUrl([FromBody] ShortenedUrlRequest request, CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var result = await shortenedUrlCommandHandler.Handle(new ShortenUrlCommand(request.Url, request.CallerId), cancellationToken);

        return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error);
    }
}