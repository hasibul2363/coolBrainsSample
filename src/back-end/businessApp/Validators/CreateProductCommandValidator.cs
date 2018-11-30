using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentValidation;
using SuitSupply.Infrastructure.Repository.Contracts;
using SuitSupply.Infrastructure.Validator.Contract;
using SuitSupply.Infrastructure.Validator.Utility;
using SuitSupply.ProductCatalog.Commands;
using SuitSupply.ProductCatalog.DomainModels;

namespace SuitSupply.ProductCatalog.Validators
{
    public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>, ISuitValidator<CreateProductCommand>
    {
        public IReadOnlyRepository ReadOnlyRepository { get; set; }
        public CreateProductCommandValidator(IReadOnlyRepository readOnlyRepository)
        {
            ReadOnlyRepository = readOnlyRepository;
            RuleFor(p => p.Id).NotEmpty();
            RuleFor(p => p.Code).NotEmpty().Must(Unique).WithMessage("Product code already exist");
            RuleFor(p => p.Price).NotEmpty().GreaterThan(0);
            RuleFor(p => p.Name).NotEmpty();
        }

        private bool Unique(string productCode) =>
            !ReadOnlyRepository.GetItems<Product>().Any(p => p.Code == productCode);

        public SuitValidationResult PerformValidation(CreateProductCommand command) => 
            Validate(command).ToSuitValidationResult();

    }
}
