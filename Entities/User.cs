using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Catalog_API.Enums;

namespace Catalog_API.Entities
{
    public record User
    {
        [Key]
        [Column("UserId")]
        public Guid Id { get; init; }

        [Required(ErrorMessage = "CreatedAt is required.")]
        public DateTimeOffset CreatedAt { get; init; }

        public DateTime? UpdatedAt { get; init; }

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email format.")]
        public string Email { get; init; }

        [Required(ErrorMessage = "PasswordHash is required.")]
        public string PasswordHash { get; set; }

        public string RefreshToken { get; set; } = string.Empty;

        public string? EmailToken { get; init; }

        public string? ResetToken { get; init; }

        public DateTime? ResetTokenExpiresAt { get; init; }

        public bool? EmailValid { get; init; }

        [Required(ErrorMessage = "FirstName is required.")]
        public string FirstName { get; init; }

        [Required(ErrorMessage = "LastName is required.")]
        public string LastName { get; init; }

        public int? FailedSignInAttempts { get; init; }

        [Required(ErrorMessage = "At least one role is required.")]
        public UserRole[] Roles { get; init; }
    }
}

