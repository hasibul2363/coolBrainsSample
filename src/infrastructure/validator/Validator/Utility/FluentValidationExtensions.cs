
using FluentValidation.Results;
using SuitSupply.Infrastructure.Validator.Contract;

namespace SuitSupply.Infrastructure.Validator.Utility
{
    public static class FluentValidationExtensions
    {
        public static SuitValidationResult ToSuitValidationResult(this ValidationResult validationResult)
        {
            var suitValidationResult = new SuitValidationResult();
            if (validationResult == null)
                return suitValidationResult;

            foreach (var validationFailure in validationResult.Errors)
                suitValidationResult.AddError(validationFailure.ErrorMessage, validationFailure.PropertyName, validationFailure.ErrorCode, validationFailure.ResourceName);

            return suitValidationResult;
        }
    }
}
