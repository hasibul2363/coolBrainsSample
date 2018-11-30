using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using ClosedXML.Excel;
using SuitSupply.Infrastructure.Bus.Contracts.Query;
using SuitSupply.Infrastructure.Bus.Query;
using SuitSupply.Infrastructure.Repository.Contracts;
using SuitSupply.Infrastructure.Validator.Contract;
using SuitSupply.ProductCatalog.DomainModels;
using SuitSupply.ProductCatalog.Queries;

namespace SuitSupply.ProductCatalog.QueryHandlers
{
    public class ExcelExportQueryHandler : SuitQueryHandler<MemoryStream, ExcelExportQuery>
    {

        public IReadOnlyRepository Repository { get; set; }

        public ExcelExportQueryHandler(IReadOnlyRepository repository)
        {
            Repository = repository;
        }
        public override Task<SuitValidationResult> Validate(ExcelExportQuery query)
        {
            return Task.FromResult(new SuitValidationResult());
        }

        public override async Task<QueryResponse<MemoryStream>> Handle(ExcelExportQuery query)
        {
            var products = Repository.GetItems<Product>();
            if (Guid.Empty != query.Id)
                products = products.Where(p => p.Id == query.Id);
            if (!string.IsNullOrEmpty(query.Name))
                products = products.Where(p => p.Name.Contains(query.Name));
            if (!string.IsNullOrEmpty(query.Code))
                products = products.Where(p => p.Code == query.Code);



            var dt = BuildDataTable(products.ToList());
            using (var wb = new XLWorkbook())
            {
                wb.Worksheets.Add(dt);
                var stream = new MemoryStream();
                wb.SaveAs(stream);
                stream.Seek(0, SeekOrigin.Begin);
                return new QueryResponse<MemoryStream>
                {
                    Success = true,
                    Data = stream,
                };

            }
        }

        private DataTable BuildDataTable(List<Product> products)
        {
            var dt = new DataTable("Products");
            dt.Columns.AddRange(new[] { new DataColumn("Id"),
                new DataColumn("Name"),
                new DataColumn("PhotoUrl"),
                new DataColumn("Price"),
                new DataColumn("LastUpdated"),
                new DataColumn("Code")
            });
            products.ForEach(product => dt.Rows.Add(product.Id, product.Name, product.PhotoUrl, product.Price, product.LastUpdated, product.Code));
            return dt;
        }

    }
}
