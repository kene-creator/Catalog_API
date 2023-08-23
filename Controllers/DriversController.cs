using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Catalog_API.Data;
using Catalog_API.Models;
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

        public DriversController(ILogger<DriversController> logger, ApiDbContext dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
        }

        [HttpGet("getAllDrivers")]
        public async Task<ActionResult> Get(){
            var driver = new Driver() 
            {
                DriverNumber = 44,
                Name = "Ahmed",
            };
            _dbContext.Drivers.Add(driver);
            await _dbContext.SaveChangesAsync();

            var allDrivers = await _dbContext.Drivers.ToListAsync();
            return Ok(allDrivers);
        }
    }
}