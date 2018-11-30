using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation;
using SuitSupply.Infrastructure.Validator.Contract;
using SuitSupply.Infrastructure.Validator.Utility;
using SuitSupply.ProductCatalog.Commands;

namespace SuitSupply.ProductCatalog.Validators
{
    public class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>, ISuitValidator<UpdateProductCommand>
    {

        public UpdateProductCommandValidator()
        {
            RuleFor(p => p.Id).NotEmpty();
            RuleFor(p => p.Price).NotEmpty().GreaterThan(0);
            RuleFor(p => p.Name).NotEmpty();
        }

        public SuitValidationResult PerformValidation(UpdateProductCommand command) =>
            Validate(command).ToSuitValidationResult();

    }
}
