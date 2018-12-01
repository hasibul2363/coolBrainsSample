using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SuitSupply.Infrastructure.Bus.Contracts;
using SuitSupply.Infrastructure.Bus.Contracts.Command;
using SuitSupply.Infrastructure.Logger.Contracts;
using SuitSupply.ProductCatalog.Commands;

namespace SuitSupply.ProductCatalog.WebService.Controllers
{
    public class CommandController : Controller
    {
        public ISuitBus Bus { get; set; }
        public ISuitLog Log { get; set; }

        public CommandController(ISuitBus bus, ISuitLog log)
        {
            Bus = bus;
            Log = log;
        }

        [HttpPost]
        public Task<CommandResponse> CreateProduct([FromBody] CreateProductCommand command)
        {
            return Bus.Send(command);
        }

        [HttpPost]
        public Task<CommandResponse> UpdateProduct([FromBody]UpdateProductCommand command)
        {
            return Bus.Send(command);
        }

        [HttpPost]
        public Task<CommandResponse> DeleteProduct([FromBody]DeleteProductCommand command)
        {
            return Bus.Send(command);
        }
    }
}