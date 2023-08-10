using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Catalog_API.Entities;

namespace Catalog_API.Repositories
{
    public interface IUsersRepository<T>
    {
         Task<User> RegisterUserAsync(User user);

        // Log in a user
        Task<T> LoginUserAsync(string email, string password);

        // Log out a user
        Task LogoutUserAsync(Guid userId);

        // Get a user by email
        Task<User> GetUserByEmailAsync(string email);

        // Get a user by ID
        Task<User> GetUserByIdAsync(Guid userId);

        // Update user information
        Task UpdateUserAsync(User user);

        // Delete a user
        Task DeleteUserAsync(Guid userId);

        bool VerifyPassword(string providedPassword, string actualPasswordHash);

        string GenerateJwtToken(User user);
    }
}