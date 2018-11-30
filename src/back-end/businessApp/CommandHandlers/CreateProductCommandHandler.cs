using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using SuitSupply.Infrastructure.Bus.Command;
using SuitSupply.Infrastructure.Bus.Contracts.Command;
using SuitSupply.Infrastructure.Logger.Contracts;
using SuitSupply.Infrastructure.Repository.Contracts;
using SuitSupply.Infrastructure.Validator.Contract;
using SuitSupply.ProductCatalog.Commands;
using SuitSupply.ProductCatalog.DomainModels;

namespace SuitSupply.ProductCatalog.CommandHandlers
{
    public class CreateProductCommandHandler : SuitCommandHandler<CreateProductCommand>
    {
        public ISuitLog Log { get; set; }
        public IRepository Repository { get; set; }
        public ISuitValidator<CreateProductCommand> Validator { get; set; }
        public CreateProductCommandHandler(ISuitValidator<CreateProductCommand> validator, IRepository repository, ISuitLog log)
        {
            Repository = repository;
            Validator = validator;
            Log = log;
        }
        public override Task<SuitValidationResult> Validate(CreateProductCommand command)
        {
            return Task.FromResult(Validator.PerformValidation(command));
        }

        public override async Task<CommandResponse> Handle(CreateProductCommand command)
        {
            var product = AutoMapper.Mapper.Map<CreateProductCommand, Product>(command);
            product.LastUpdated = DateTime.UtcNow;
            Repository.Add(product);
            await Repository.SaveChanges();
            Log.Information("Crate Product Command has handled successfully with command {@command}",command);
            return new CommandResponse
            {
                Success = true
            };
        }
    }
}
