using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Catalog_API.Entities;
using Catalog_API.Utility;

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
        Task<User> GetUserByRefreshTokenAsync(string refreshToken);

        // Update user information
        Task UpdateUserAsync(User user);

        // Delete a user
        Task DeleteUserAsync(Guid userId);

        bool VerifyPassword(string providedPassword, string actualPasswordHash);

        (string AccessToken, string RefreshToken) GenerateTokens(User user);

        Task<List<User>> GetAllUsersAsync();

        void SetRefreshToken(RefreshToken refreshToken);
        bool ValidateRefreshToken(string refreshToken);
    }
}