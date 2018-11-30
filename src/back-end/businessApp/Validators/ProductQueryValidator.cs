using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation;
using SuitSupply.Infrastructure.Validator.Contract;
using SuitSupply.Infrastructure.Validator.Utility;
using SuitSupply.ProductCatalog.Queries;

namespace SuitSupply.ProductCatalog.Validators
{
    public class ProductQueryValidator : AbstractValidator<ProductQuery>, ISuitValidator<ProductQuery>
    {
        public ProductQueryValidator()
        {
            RuleFor(p => p.PageNumber).GreaterThanOrEqualTo(1);
            RuleFor(p => p.PageSize).GreaterThanOrEqualTo(1).LessThanOrEqualTo(100);
        }
        public SuitValidationResult PerformValidation(ProductQuery query)
        {
            return Validate(query).ToSuitValidationResult();
        }
    }
}
