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
    public class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>, ISuitValidator<UpdateProductCommand>
    {
        public IReadOnlyRepository Repository { get; set; }


        public UpdateProductCommandValidator(IReadOnlyRepository readOnlyRepository)
        {
            Repository = readOnlyRepository;
            RuleFor(p => p.Id).NotEmpty();
            RuleFor(p => p.Name).NotEmpty();
            RuleFor(p => p.Price).NotEmpty().GreaterThan(0);
            RuleFor(p => p.Code).NotEmpty();
            RuleFor(p => p).Must(ProductCodeUnique).WithMessage("Product code already exist");
        }

        private bool ProductCodeUnique(UpdateProductCommand command) =>
             !Repository.GetItems<Product>(p => p.Id != command.Id && p.Code == command.Code).Any();


        public SuitValidationResult PerformValidation(UpdateProductCommand command) =>
            Validate(command).ToSuitValidationResult();

    }
}
