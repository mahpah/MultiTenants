using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MultiTenants.Data;
using MultiTenants.Data.Models;
using MultiTenants.Tenant.Model;

namespace MultiTenants.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly AppTenant _tenant;

        public ValuesController(ApplicationDbContext dbContext, AppTenant tenant)
        {
            _dbContext = dbContext;
            _tenant = tenant;
        }

        // GET api/values
        [HttpGet]
        public async Task<IEnumerable<Value>> Get()
        {
            var list = await _dbContext.Values.ToListAsync();
            return list;
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public async Task Post()
        {
            await _dbContext.AddAsync(new Value
            {
                Content = Guid.NewGuid().ToString()
            });

            await _dbContext.SaveChangesAsync();
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }

        [HttpGet("whoami")]
        public IActionResult GetTenant()
        {
            if (_tenant is null)
                return NotFound();

            return Ok(_tenant);
        }
    }
}
