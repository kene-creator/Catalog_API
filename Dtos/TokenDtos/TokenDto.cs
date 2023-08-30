
using Newtonsoft.Json;

namespace Catalog_API.Dtos.TokenDtos
{
    public record TokenDto
    {
        public string AccessToken { get; init; }

        public string RefreshToken { get; init; }
    }
}

