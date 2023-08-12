using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Catalog_API.Dtos.UserDtos
{
    public class CreateUserDto
    {  
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email format")]
        public required string Email { get; init; }

        [Required(ErrorMessage = "Password is required")]
        [MinLength(8, ErrorMessage = "Password must be at least 8 characters long")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).+$", 
        ErrorMessage = "Password must contain at least one lowercase letter, one uppercase letter, and one digit")]
        public required string Password { get; init; }
        
        [Required(ErrorMessage = "First name is required")]
        public required string FirstName { get; init; }
        
        [Required(ErrorMessage = "Last name is required")]
        public required string LastName { get; init; }
        
        [Range(0, 5, ErrorMessage = "Failed sign-in attempts exceeded")]
        public int? FailedSignInAttempts { get; init; } = 0;
    }
}