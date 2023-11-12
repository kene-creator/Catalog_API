using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Catalog_API.Dtos;
using Catalog_API.Dtos.UserDtos;
using Catalog_API.Entities;
using Catalog_API.Enums;
using Catalog_API.Interface;
using Catalog_API.Utility;

namespace Catalog_API
{
    public static class Extensions
    {
        public static ItemDto AsItemDto(this Item item)
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

        public static void ConfigureCors(this IServiceCollection services) =>
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy", builder =>
                    builder.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader());
            });

        public static void ConfigureIISIntegration(this IServiceCollection services) =>
            services.Configure<IISOptions>(options => { });

        public static void ConfigureLoggerService(this IServiceCollection services) =>
            services.AddSingleton<ILoggerManager, LoggerManager>();
    }
}