using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SuitSupply.Infrastructure.Bus.Contracts.Query;
using SuitSupply.Infrastructure.Bus.Query;
using SuitSupply.Infrastructure.Repository.Contracts;
using SuitSupply.Infrastructure.Validator.Contract;
using SuitSupply.ProductCatalog.DomainModels;
using SuitSupply.ProductCatalog.Queries;

namespace SuitSupply.ProductCatalog.QueryHandlers
{
    public class ProductQueryHandler : SuitQueryHandler<List<Product>, ProductQuery>
    {
        public ISuitValidator<ProductQuery> Validator { get; set; }
        public IReadOnlyRepository Repository { get; set; }

        public ProductQueryHandler(ISuitValidator<ProductQuery> validator, IReadOnlyRepository repository)
        {
            Validator = validator;
            Repository = repository;
        }
        public override Task<SuitValidationResult> Validate(ProductQuery query)
        {
            return Task.FromResult(Validator.PerformValidation(query));
        }

        public override async Task<QueryResponse<List<Product>>> Handle(ProductQuery query)
        {
            var products = Repository.GetItems<Product>();
            if (Guid.Empty != query.Id)
                products = products.Where(p => p.Id == query.Id);
            if (!string.IsNullOrEmpty(query.Name))
                products = products.Where(p => p.Name.Contains(query.Name));
            if (!string.IsNullOrEmpty(query.Code))
                products = products.Where(p => p.Code == query.Code);

            var count = products.Count();
            var skip = (query.PageNumber - 1) * query.PageSize;
            products = products.Skip(skip).Take(query.PageSize);

            return new QueryResponse<List<Product>>
            {
                Success = true,
                Data = products.ToList(),
                TotalCount = count
            };
        }
    }
}
