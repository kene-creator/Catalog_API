using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Catalog_API.Enums;

namespace Catalog_API.Dtos.UserDtos
{
    public record UserDto
    {
         public Guid Id { get; init; }
         public string Email { get; init; }
         public string FirstName { get; init; }
         public string LastName { get; init; }
         public UserRole[] Roles { get; init; }

    }
}