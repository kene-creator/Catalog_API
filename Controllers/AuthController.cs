using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Catalog_API.Dtos.TokenDtos;
using Catalog_API.Dtos.UserDtos;
using Catalog_API.Entities;
using Catalog_API.Enums;
using Catalog_API.Repositories;
using Catalog_API.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Catalog_API.Controllers
{
    [ApiController]
    [Route("auth")]
    public class AuthController : ControllerBase
    {
        private readonly IUsersRepository<AuthenticationResult> _userRepository;

        public AuthController(IUsersRepository<AuthenticationResult> userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpPost("register")]
        public async Task<ActionResult<CreateUserDto>> RegisterUserAsync([FromBody] CreateUserDto createUserDto)
        {
            var user = new User
            {
                Id = Guid.NewGuid(),
                Email = createUserDto.Email,
                PasswordHash = createUserDto.Password,
                CreatedAt = DateTimeOffset.UtcNow,
                FirstName = createUserDto.FirstName,
                LastName = createUserDto.LastName,
                Roles = new UserRole[] { UserRole.User }
            };
            var registeredUser = await _userRepository.RegisterUserAsync(user);

            if (registeredUser == null)
            {
                return BadRequest("Failed to register user");
            }

            return CreatedAtAction(nameof(GetUserAsync), new { id = user.Id }, user.AsUserDto());
        }

        [HttpPost("login")]
        public async Task<ActionResult<LoginUserDto>> LoginAsync([FromBody] LoginUserDto loginUserDto)
        {
            var user = await _userRepository.GetUserByEmailAsync(loginUserDto.Email);

            if (user == null || !_userRepository.VerifyPassword(loginUserDto.Password, user.PasswordHash))
            {
                return BadRequest("Email or password incorrect");
            }

            var token = _userRepository.GenerateTokens(user);
            user.RefreshToken = token.RefreshToken;
            _userRepository.SetRefreshToken(new RefreshToken
            {
                Token = token.RefreshToken,
                Expires = DateTime.UtcNow.AddDays(30) // Set the cookie expiration time
            });

            return Ok(new { access_token = token.AccessToken, user });
        }

        [HttpGet("user/{guid:guid}")]
        public async Task<ActionResult<UserDto>> GetUserAsync(Guid guid)
        {
            var user = await _userRepository.GetUserByIdAsync(guid);

            if (user == null)
            {
                return NotFound();
            }

            return user.AsUserDto();
        }

        [HttpGet("users")]
        [Authorize]
        public async Task<ActionResult<List<User>>> GetUsersAsync()
        {
            var users = await _userRepository.GetAllUsersAsync();
            return users;
        }
        
        [HttpPost("refresh-token")]
        public async Task<ActionResult<TokenDto>> RefreshTokenAsync()
        {
            // Get the refresh token from the HTTP request cookies
            var refreshToken = Request.Cookies["refreshToken"];

            if (string.IsNullOrEmpty(refreshToken))
            {
                return BadRequest("Refresh token not found in cookies");
            }

            if (!_userRepository.ValidateRefreshToken(refreshToken))
            {
                return BadRequest("Invalid refresh token");
            }

            var user = await _userRepository.GetUserByRefreshTokenAsync(refreshToken);
            if (user == null)
            {
                return BadRequest("User not found for the given refresh token");
            }

            var newTokens = _userRepository.GenerateTokens(user);

            return Ok(new TokenDto
            {
                AccessToken = newTokens.AccessToken,
                RefreshToken = newTokens.RefreshToken
            });
        }

    }
}