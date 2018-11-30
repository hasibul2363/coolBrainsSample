using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SuitSupply.Infrastructure.Bus.Contracts;
using SuitSupply.ProductCatalog.Commands;

namespace SuitSupply.ProductCatalog.WebService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        public ISuitBus SuitBus { get; set; }
        public ValuesController(ISuitBus suitBus)
        {
            SuitBus = suitBus;
        }

        // GET api/values
        [HttpGet]
        public async Task<ActionResult<IEnumerable<string>>> Get()
        {
            var command = new CreateProductCommand
            {
                Id = Guid.NewGuid(), Code = Guid.NewGuid().ToString(),Price = 256, Name = "PP", PhotoUrl = "photo"
            };

            var response = await SuitBus.Send(command);
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
