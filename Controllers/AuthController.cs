using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Catalog_API.Dtos.UserDtos;
using Catalog_API.Entities;
using Catalog_API.Enums;
using Catalog_API.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Catalog_API.Controllers
{
     [ApiController]
     [Route("auth")]
    public class AuthController : ControllerBase
    {
        private readonly IUsersRepository<AuthenticationResult> _userRepository;

        public AuthController(IUsersRepository<AuthenticationResult>  userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpPost("register")]
        public async Task<ActionResult<CreateUserDto>> RegisterUserAsync(CreateUserDto createUserDto) {

            var user = new User {
                Id = Guid.NewGuid(),
                Email = createUserDto.Email,
                PasswordHash = createUserDto.Password,
                CreatedAt = DateTimeOffset.UtcNow,
                FirstName = createUserDto.FirstName,
                LastName = createUserDto.LastName,
                Roles = new UserRole[] { UserRole.User } 
            };
            var registeredUser = await _userRepository.RegisterUserAsync(user);

            if(registeredUser == null) {
                return BadRequest("Failed to register user");
            }
            return CreatedAtAction(nameof(GetUserAsync), new { id = user.Id }, user.AsUserDto());
        }

        [HttpPost("login")]
        public async Task<ActionResult<LoginUserDto>> LoginAsync(LoginUserDto loginUserDto){
         var user = await _userRepository.GetUserByEmailAsync(loginUserDto.Email);

         if(user != null && _userRepository.VerifyPassword(loginUserDto.Password, user.PasswordHash)) {
            var token = _userRepository.GenerateJwtToken(user);
            return Ok(new { access_token = token, user });
         }
         return BadRequest("Email or password incorrect");   
        }

        [HttpGet("user/{id}")]
        public async Task<ActionResult<UserDto>> GetUserAsync(Guid id)
        {
            var user = await _userRepository.GetUserByIdAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return user.AsUserDto();
        }
    }

}