using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SuitSupply.Infrastructure.Bus.Contracts;
using SuitSupply.Infrastructure.Bus.Contracts.Query;
using SuitSupply.Infrastructure.Logger.Contracts;
using SuitSupply.ProductCatalog.DomainModels;
using SuitSupply.ProductCatalog.Queries;

namespace SuitSupply.ProductCatalog.WebService.Controllers
{
    public class QueryController : Controller
    {
        public ISuitBus Bus { get; set; }
        public ISuitLog Log { get; set; }


        public QueryController(ISuitBus bus, ISuitLog log)
        {
            Bus = bus;
            Log = log;
        }
        public Task<QueryResponse<List<Product>>> ProductQuery([FromBody] ProductQuery query)
        {
            return Bus.Query(query);
        }
    }
}