using SuitSupply.Infrastructure.Bus.Command;
using SuitSupply.Infrastructure.Bus.Contracts.Command;
using SuitSupply.Infrastructure.Logger.Contracts;
using SuitSupply.Infrastructure.Repository.Contracts;
using SuitSupply.Infrastructure.Validator.Contract;
using SuitSupply.ProductCatalog.Commands;
using SuitSupply.ProductCatalog.DomainModels;
using System.Threading.Tasks;

namespace SuitSupply.ProductCatalog.CommandHandlers
{
    class DeleteProductCommandHandler : SuitCommandHandler<DeleteProductCommand>
    {
        public ISuitLog Log { get; set; }
        public IRepository Repository { get; set; }
        public ISuitValidator<DeleteProductCommand> Validator { get; set; }
        public DeleteProductCommandHandler(ISuitValidator<DeleteProductCommand> validator, IRepository repository, ISuitLog log)
        {
            Repository = repository;
            Validator = validator;
            Log = log;
        }
        public override Task<SuitValidationResult> Validate(DeleteProductCommand command)
        {
            return Task.FromResult(Validator.PerformValidation(command));
        }

        public override async Task<CommandResponse> Handle(DeleteProductCommand command)
        {
            Repository.Remove<Product>(p=>p.Id == command.Id);
            await Repository.SaveChanges();
            Log.Information("Delete Product Command has handled successfully with details {@command}", command);
            return new CommandResponse
            {
                Success = true
            };
        }
    }
}
