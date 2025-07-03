using System.ComponentModel.DataAnnotations;

namespace UrlShortener.Api.Dtos.UserDtos;

public class SignupRequest
{
    [Required]
    public string Login { get; set; } = default!;

    [Required, MinLength(6)]
    public string Password { get; set; } = default!;
}