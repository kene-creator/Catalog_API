using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Catalog_API.Entities;
using Microsoft.IdentityModel.Tokens;
using MongoDB.Driver;
using BCrypt.Net;

namespace Catalog_API.Repositories
{

    public class AuthenticationResult
{
    public  User User { get; set; }
    public string ErrorMessage { get; set; }
}
  public class MongoDbUsersRepository : IUsersRepository<AuthenticationResult>
{
    private const string databaseName = "usersdb";
    private const string collectionName = "users";
    private readonly IMongoCollection<User> usersCollection;
    private readonly FilterDefinitionBuilder<User> filterBuilder = Builders<User>.Filter;
    private readonly IConfiguration _configuration;

    public MongoDbUsersRepository(IMongoClient mongoClient, IConfiguration configuration)
    {
        IMongoDatabase database = mongoClient.GetDatabase(databaseName);
        usersCollection = database.GetCollection<User>(collectionName);
        _configuration = configuration;
    }

    public async Task<User> RegisterUserAsync(User user)
    {
        string hashedPassword = HashPassword(user.PasswordHash);
        user.PasswordHash = hashedPassword;
        await usersCollection.InsertOneAsync(user);
        return user;
    }

   public async Task<AuthenticationResult> LoginUserAsync(string email, string password)
    {
    var filter = filterBuilder.Eq(user => user.Email, email);
    var user = await usersCollection.Find(filter).SingleOrDefaultAsync();

    if (user != null && VerifyPassword(password, user.PasswordHash))
    {
        return new AuthenticationResult { User = user };
    }

    return new AuthenticationResult { ErrorMessage = "Email or password incorrect" };
    }

    public async Task LogoutUserAsync(Guid userId)
    {
        // Implement logout logic if needed
    }

    public async Task<User> GetUserByEmailAsync(string email)
    {
        var filter = filterBuilder.Eq(user => user.Email, email);
        return await usersCollection.Find(filter).SingleOrDefaultAsync();
    }

    public async Task<User> GetUserByIdAsync(Guid userId)
    {
        var filter = filterBuilder.Eq(user => user.Id, userId);
        return await usersCollection.Find(filter).SingleOrDefaultAsync();
    }

    public async Task<List<User>> GetAllUsersAsync()
    {
        var filter = Builders<User>.Filter.Empty;
        return await usersCollection.Find(filter).ToListAsync();
    }


    public async Task UpdateUserAsync(User user)
    {
        var filter = filterBuilder.Eq(existingUser => existingUser.Id, user.Id);
        await usersCollection.ReplaceOneAsync(filter, user);
    }

    public async Task DeleteUserAsync(Guid userId)
    {
        var filter = filterBuilder.Eq(user => user.Id, userId);
        await usersCollection.DeleteOneAsync(filter);
    }

    public bool VerifyPassword(string providedPassword, string actualPasswordHash)
    {
       return BCrypt.Net.BCrypt.Verify(providedPassword, actualPasswordHash);
    }

    public string HashPassword(string password)
    {
        // Hash the password using BCrypt
        var salt = BCrypt.Net.BCrypt.GenerateSalt();
        var hashedPassword = BCrypt.Net.BCrypt.HashPassword(password, salt);
        return hashedPassword;
    }

    public string GenerateJwtToken(User user)
    {
        var claims = new []
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Email, user.Email)
        };

        var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_configuration.GetSection("Jwt:Key").Value));
        
       var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
       var issuer = _configuration.GetSection("Jwt:Issuer").Value;

       var token = new JwtSecurityToken(issuer,
            issuer,
            claims: claims,
            expires: DateTime.Now.AddDays(7),
            signingCredentials: credentials
       );

       return new JwtSecurityTokenHandler().WriteToken(token);

    }
    }


}