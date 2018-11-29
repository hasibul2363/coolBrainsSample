using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation;
using SuitSupply.Infrastructure.Validator.Contract;
using SuitSupply.Infrastructure.Validator.Utility;
using SuitSupply.ProductCatalog.Commands;

namespace SuitSupply.ProductCatalog.Validators
{
    public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>, ISuitValidator<CreateProductCommand>
    {
        public CreateProductCommandValidator()
        {
            RuleFor(p => p.Id).NotEmpty();
            RuleFor(p => p.Code).NotEmpty().Must(Unique);
            RuleFor(p => p.Price).NotEmpty().GreaterThan(0);
            RuleFor(p => p.Name).NotEmpty();
        }

        private bool Unique(string productCode)
        {
            return true;
        }

        public SuitValidationResult PerformValidation(CreateProductCommand command)
        {
            return Validate(command).ToSuitValidationResult();
        }
    }
}
