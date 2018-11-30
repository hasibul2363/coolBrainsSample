using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SuitSupply.Infrastructure.Bus.Contracts;
using SuitSupply.ProductCatalog.Commands;

namespace SuitSupply.ProductCatalog.WebService.Controllers
{
    //[ApiVersion("1.0")]
    //[Route("api/v{version:apiVersion}/[controller]")]
    //[ApiController]
    public class ValuesController : Controller
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
            
            return new string[] { "running v1.." };
        }
    }


    //[ApiVersion("2.0")]
    //[Route("api/v{version:apiVersion}/Values")]
    //[ApiController]
    public class ValuesV2Controller : Controller
    {
        public ISuitBus SuitBus { get; set; }
        public ValuesV2Controller(ISuitBus suitBus)
        {
            SuitBus = suitBus;
        }

        // GET api/values
        [HttpGet]
        public async Task<ActionResult<IEnumerable<string>>> Get()
        {

            return new string[] { "running v2.." };
        }
    }
}
