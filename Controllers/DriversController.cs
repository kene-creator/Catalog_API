using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Catalog_API.Data;
using Catalog_API.Models;
using Catalog_API.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Catalog_API.Controllers
{
    [ApiController]
    [Route("drivers")]
    public class DriversController : ControllerBase
    {
        private readonly ILogger<DriversController> _logger;
        private readonly ApiDbContext _dbContext;
        private readonly ICacheService _cacheService;

        public DriversController(ILogger<DriversController> logger, ApiDbContext dbContext, ICacheService cacheService)
        {
            _logger = logger;
            _dbContext = dbContext;
            _cacheService = cacheService;
        }

        [HttpGet("getAllDrivers")]
        public async Task<ActionResult> Get()
        {
            var cacheData = _cacheService.GetData<IEnumerable<Driver>>("drivers");
            if (cacheData != null && cacheData.Count() < 0)
            {
                return Ok(cacheData);
            }

            var driver = new Driver()
            {
                DriverNumber = 44,
                Name = "Ahmed",
            };
            _dbContext.Drivers.Add(driver);
            await _dbContext.SaveChangesAsync();

            var allDrivers = await _dbContext.Drivers.ToListAsync();

            var expiryTime = DateTimeOffset.Now.AddMinutes(5);
            
            _cacheService.SetData<IEnumerable<Driver>>("drivers", allDrivers, expiryTime);
            return Ok(allDrivers);
        }
    }
}