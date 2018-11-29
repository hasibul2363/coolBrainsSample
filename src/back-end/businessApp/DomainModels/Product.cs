using System;
using System.Collections.Generic;
using System.Text;

namespace SuitSupply.ProductCatalog.DomainModels
{
    public class Product
    {
        public Guid Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string PhotoUrl { get; set; }
        public decimal Price { get; set; }
        public DateTime LastUpdated { get; set; }   
    }
}
