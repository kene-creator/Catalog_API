using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Catalog_API.Dtos;
using Catalog_API.Dtos.UserDtos;
using Catalog_API.Entities;
using Catalog_API.Enums;

namespace Catalog_API
{
    public static class Extensions
    {
        public static ItemDto AsItemDto(this Entities.Item item)
        {
            return new ItemDto
            {
                Id = item.Id,
                Name = item.Name,
                Price = item.Price,
                CreatedDate = item.CreatedDate
            };
        }

         public static UserDto AsUserDto(this User user)
        {
            return new UserDto
            {
                Id = user.Id,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Roles = user.Roles
            };
        }   
    }
}