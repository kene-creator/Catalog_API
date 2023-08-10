using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Catalog_API.Enums;

namespace Catalog_API.Entities
{
    public record User
    {
         public Guid Id { get; init; }
        public DateTimeOffset CreatedAt { get; init; }
        public DateTime? UpdatedAt { get; init; }
        public string Email { get; init; }
        public string PasswordHash { get; set; }
        public string? EmailToken { get; init; }
        public string? ResetToken { get; init; }
        public DateTime? ResetTokenExpiresAt { get; init; }
        public bool? EmailValid { get; init; }
        public string FirstName { get; init; }
        public string LastName { get; init; }
        public int? FailedSignInAttempts { get; init; }
        public UserRole[] Roles { get; init; }
    }
}