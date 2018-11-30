using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation;
using SuitSupply.Infrastructure.Validator.Contract;
using SuitSupply.Infrastructure.Validator.Utility;
using SuitSupply.ProductCatalog.Commands;

namespace SuitSupply.ProductCatalog.Validators
{
    public class DeleteProductCommandValidator : AbstractValidator<DeleteProductCommand>, ISuitValidator<DeleteProductCommand>
    {

        public DeleteProductCommandValidator()
        {
            RuleFor(p => p.Id).NotEmpty();
        }

        public SuitValidationResult PerformValidation(DeleteProductCommand command) =>
            Validate(command).ToSuitValidationResult();

    }
}
