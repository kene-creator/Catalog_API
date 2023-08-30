namespace Catalog_API.Dtos.TokenDtos;

public record RefreshTokenRequestDto
{
    public string RefreshToken { get; set; }
}