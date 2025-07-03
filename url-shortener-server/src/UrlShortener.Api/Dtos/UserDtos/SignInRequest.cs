using System.ComponentModel.DataAnnotations;

namespace UrlShortener.Api.Dtos.UserDtos;

public class SignInRequest
{
    [Required]
    public string Login { get; set; } = default!;

    [Required]
    public string Password { get; set; } = default!;
}