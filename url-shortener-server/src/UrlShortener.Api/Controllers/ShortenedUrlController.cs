using Microsoft.AspNetCore.Mvc;
using UrlShortener.Api.Dtos.ShortenedUrlDtos;
using UrlShortener.Application.UseCases.ShortenedUrls.Commands.ShortenUrl;
using UrlShortener.Application.UseCases.ShortenedUrls.Queries.RedirectToOriginalUrl;
using UrlShortener.Application.UseCases.ShortenedUrls.Queries.GetAllShortenedUrls;
// using UrlShortener.Application.UseCases.ShortenedUrls.Queries.GetShortenedUrlById;
// using UrlShortener.Application.UseCases.ShortenedUrls.Commands.DeleteShortenedUrl;

namespace UrlShortener.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ShortenedUrlController(
    ShortenUrlCommandHandler shortenedUrlCommandHandler,
    GetAllShortenedUrlsQueryHandler getAllShortenedUrlsQueryHandler
    // GetShortenedUrlByIdQueryHandler getShortenedUrlByIdQueryHandler,
    // DeleteShortenedUrlCommandHandler deleteShortenedUrlCommandHandler
) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> ShortenUrl([FromBody] ShortenedUrlRequest request, CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var result = await shortenedUrlCommandHandler.Handle(new ShortenUrlCommand(request.Url, request.CallerId), cancellationToken);

        return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error);
    }

    [HttpGet]
    public async Task<IActionResult> GetPaged(
        [FromQuery] int page = 1, 
        [FromQuery] int size = 10,
        CancellationToken cancellationToken = default)
    {
        if (page < 1) page = 1;
        if (size < 1) size = 10;

        var query = new GetAllShortenedUrlsQuery(page, size);
        var result = await getAllShortenedUrlsQueryHandler.Handle(query, cancellationToken);

        return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error);
    }
    
    // [HttpGet("{id}")]
    // public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
    // {
    //     var result = await getShortenedUrlByIdQueryHandler.Handle(new GetShortenedUrlByIdQuery(id), cancellationToken);
    //     return result.IsSuccess ? Ok(result.Value) : NotFound(result.Error);
    // }
    //
    // [HttpDelete("{id}")]
    // public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
    // {
    //     var result = await deleteShortenedUrlCommandHandler.Handle(new DeleteShortenedUrlCommand(id), cancellationToken);
    //     return result.IsSuccess ? NoContent() : NotFound(result.Error);
    // }
}
