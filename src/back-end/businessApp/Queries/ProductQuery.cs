using System;
using System.Collections.Generic;
using System.Text;
using SuitSupply.Infrastructure.Bus.Contracts.Query;
using SuitSupply.ProductCatalog.DomainModels;

namespace SuitSupply.ProductCatalog.Queries
{
    public class ProductQuery : SuitQuery<List<Product>>
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public Guid Id { get; set; }

    }
}
