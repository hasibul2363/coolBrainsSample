using FluentValidation.Results;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SuitSupply.Infrastructure.Validator.Utility;

namespace SuitSupply.Infrastructure.Validator.Test
{
    [TestClass]
    public class FluentValidationExtensionsTest
    {
        [TestMethod]
        public void ToSuitValidationResultMustSucceed()
        {
            var ecapValidationResult = new ValidationResult().ToSuitValidationResult();
        }
    }
}
