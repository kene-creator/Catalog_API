using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Catalog_API.Dtos;

namespace Catalog_API
{
    public static class Extensions
    {
        public static ItemDto AsDto(this Entities.Item item)
        {
            return new ItemDto
            {
                Id = item.Id,
                Name = item.Name,
                Price = item.Price,
                CreatedDate = item.CreatedDate
            };
        }   
    }
}