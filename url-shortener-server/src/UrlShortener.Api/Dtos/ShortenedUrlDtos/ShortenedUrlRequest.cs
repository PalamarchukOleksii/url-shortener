using System.ComponentModel.DataAnnotations;
using UrlShortener.Domain.Models.UserModel;

namespace UrlShortener.Api.Dtos.ShortenedUrlDtos;

public class ShortenedUrlRequest
{
    [Required]
    public string Url { get; set; } = string.Empty;
    
    [Required]
    public UserId CallerId { get; set; } = new UserId(Guid.Empty);
}