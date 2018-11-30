using SuitSupply.Infrastructure.Bus.Command;
using SuitSupply.Infrastructure.Bus.Contracts.Command;
using SuitSupply.Infrastructure.Logger.Contracts;
using SuitSupply.Infrastructure.Repository.Contracts;
using SuitSupply.Infrastructure.Validator.Contract;
using SuitSupply.ProductCatalog.Commands;
using SuitSupply.ProductCatalog.DomainModels;
using System;
using System.Threading.Tasks;

namespace SuitSupply.ProductCatalog.CommandHandlers
{
    public class UpdateProductCommandHandler : SuitCommandHandler<UpdateProductCommand>
    {
        public ISuitLog Log { get; set; }
        public IRepository Repository { get; set; }
        public ISuitValidator<UpdateProductCommand> Validator { get; set; }
        public UpdateProductCommandHandler(ISuitValidator<UpdateProductCommand> validator, IRepository repository, ISuitLog log)
        {
            Repository = repository;
            Validator = validator;
            Log = log;
        }
        public override Task<SuitValidationResult> Validate(UpdateProductCommand command)
        {
            return Task.FromResult(Validator.PerformValidation(command));
        }

        public override async Task<CommandResponse> Handle(UpdateProductCommand command)
        {
            var product = AutoMapper.Mapper.Map<UpdateProductCommand, Product>(command);
            product.LastUpdated = DateTime.UtcNow;
            Repository.Update(product);
            await Repository.SaveChanges();
            Log.Information("Update Product Command has handled successfully with command {@command}", command);
            return new CommandResponse
            {
                Success = true
            };
        }
    }
}
