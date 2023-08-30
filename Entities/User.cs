using Catalog_API.Enums;

namespace Catalog_API.Entities
{
    public record User
    {
        public Guid Id { get; init; }
        public DateTimeOffset CreatedAt { get; init; }
        public DateTime? UpdatedAt { get; init; }
        public required string Email { get; init; }
        public required string PasswordHash { get; set; }
        public string RefreshToken { get; set; } = string.Empty;
        public string? EmailToken { get; init; }
        public string? ResetToken { get; init; }
        public DateTime? ResetTokenExpiresAt { get; init; }
        public bool? EmailValid { get; init; }
        public required string FirstName { get; init; }
        public required string LastName { get; init; }
        public int? FailedSignInAttempts { get; init; }
        public required UserRole[] Roles { get; init; }
    }
}