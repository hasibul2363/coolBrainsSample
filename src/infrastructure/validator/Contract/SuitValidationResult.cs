using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SuitSupply.Infrastructure.Validator.Contract
{
    public class SuitValidationResult
    {
        public List<SuitValidationFailure> Errors { get; set; }

        public SuitValidationResult()
        {
            Errors = new List<SuitValidationFailure>();
        }

        public SuitValidationResult(List<SuitValidationFailure> errors)
        {
            Errors = errors;
        }

        public void AddError(string errorMessage, string propertyName = "", string errorCode="", string resourceName = "")
        {
            Errors.Add(new SuitValidationFailure {ErrorMessage = errorMessage, PropertyName = propertyName, ErrorCode = errorCode, ResourceName = resourceName });
        }

        public override string ToString()
        {
            var errorMessages = new StringBuilder();
            foreach (var error in Errors)
            {
                if (!string.IsNullOrEmpty(errorMessages.ToString()))
                    errorMessages.Append(",\n");

                errorMessages.Append(error.ErrorMessage);
            }

            return errorMessages.ToString();
        }

        public bool IsValid => !Errors.Any();
    }
}
