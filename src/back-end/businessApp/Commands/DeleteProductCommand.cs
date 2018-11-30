using System;
using System.Collections.Generic;
using System.Text;
using SuitSupply.Infrastructure.Bus.Contracts.Command;

namespace SuitSupply.ProductCatalog.Commands
{
    public class DeleteProductCommand : SuitCommand
    {
        public Guid Id { get; set; }
    }
}
